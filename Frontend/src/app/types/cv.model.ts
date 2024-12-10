import { PersonalInformation } from './personal-information.model';
import { ExperienceInformation } from './experience-information.model';

export class CV {
  id: number;
  name: string;
  personalInformationId?: number;
  personalInformation?: PersonalInformation;
  experiences: ExperienceInformation[];
}
