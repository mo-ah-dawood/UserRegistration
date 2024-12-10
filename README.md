This app is integrated with .Net aspire 
  ## to run this app you have tow choose one of two approaches which are exists in vs code launch file


  ### Aspire (recommended)
  - You can launch aspire host project which responsible for launching other projects
  - This will give you abilities like logging , tracing etc.
  - Also there is no need to specify url of backend in frontend, aspire project do this
  
  <img width="1663" alt="Screenshot 2024-12-10 at 5 10 21 PM" src="https://github.com/user-attachments/assets/80881d3a-00eb-4280-90b0-5dd94dbe67c5">

  ### Separated projects 
  - You can launch every project separably
  - Project one is Backend -> Api
  - Project two is Frontend
  - The launch profiles exists in vs code launch file
  > but keep in mind that frontend project contains `proxy.conf.js` you have to set api url there 
