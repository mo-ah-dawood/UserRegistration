using UserRegistration.Application.Exceptions;
using UserRegistration.Application.Interfaces;
using UserRegistration.Core.Entities;
using UserRegistration.Core.Enum;
using UserRegistration.Core.Repositories;
using UserRegistration.Core.Services;

namespace UserRegistration.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISMSSender sMSSender;
        private readonly IEmailSender emailSender;

        public UserService(IUnitOfWork unitOfWork, ISMSSender sMSSender, IEmailSender emailSender)
        {
            this.unitOfWork = unitOfWork;
            this.sMSSender = sMSSender;
            this.emailSender = emailSender;
        }


        public async Task<User> AcceptPrivacy(string userId)
        {
            var user = await GetUserProfileAsync(userId);
            user.ISPrivacyAccepted = true;
            await unitOfWork.SaveChangesAsync();
            return user;

        }

        public async Task<User> GetUserProfileAsync(string userId)
        {
            return await unitOfWork.Users.Find(userId) ?? throw new AppException("NotFound", "User not found.");
        }

        public async Task<User> Login(string icNumber)
        {
            var user = await unitOfWork.Users.GetUserByICNumberAsync(icNumber) ?? throw new AppException("NotFound", "User not exists.");
            var transaction = await unitOfWork.GetTransition();
            var smsVerification = await unitOfWork.Verification.CreateNewVerification(user.Id, VerificationType.PhoneNumber);
            var emailVerification = await unitOfWork.Verification.CreateNewVerification(user.Id, VerificationType.EmailAddress);
            await unitOfWork.SaveChangesAsync();
            await emailSender.SendVerificationEmail(user, emailVerification.Code);
            await sMSSender.SendVerificationSMS(user, smsVerification.Code);
            await transaction.Commit();
            return user;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            if (await unitOfWork.Users.GetUserByICNumberAsync(user.ICNumber) != null)
            {
                throw new AppException("AlreadyExists", "User with this IC number already exists.");
            }
            var transaction = await unitOfWork.GetTransition();
            var smsVerification = await unitOfWork.Verification.CreateNewVerification(user.Id, VerificationType.PhoneNumber);
            var emailVerification = await unitOfWork.Verification.CreateNewVerification(user.Id, VerificationType.EmailAddress);
            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            await emailSender.SendVerificationEmail(user, emailVerification.Code);
            await sMSSender.SendVerificationSMS(user, smsVerification.Code);
            await transaction.Commit();
            return user;
        }

        public async Task<User> CreatePinAsync(string userId, string pinCode)
        {
            var user = await GetUserProfileAsync(userId);
            user.PinCode = pinCode;
            await unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<User> VerifyEmailAsync(string icNumber, string verificationCode)
        {
            var user = await unitOfWork.Users.GetUserByICNumberAsync(icNumber) ?? throw new AppException("NotFound", "User not exists.");
            var verification = await unitOfWork.Verification.GetVerificationAsync(user.Id, verificationCode, VerificationType.EmailAddress) ?? throw new AppException("InvalidCode", "Invalid or expired verification code.");
            unitOfWork.Verification.Delete(verification);
            user.IsEmailVerified = true;
            await unitOfWork.SaveChangesAsync();
            return user;

        }

        public async Task<User> VerifyPhoneAsync(string icNumber, string verificationCode)
        {
            var user = await unitOfWork.Users.GetUserByICNumberAsync(icNumber) ?? throw new AppException("NotFound", "User not exists.");
            var verification = await unitOfWork.Verification.GetVerificationAsync(user.Id, verificationCode, VerificationType.PhoneNumber) ?? throw new AppException("InvalidCode", "Invalid or expired verification code.");
            unitOfWork.Verification.Delete(verification);
            user.IsPhoneVerified = true;
            await unitOfWork.SaveChangesAsync();
            return user;
        }


    }
}