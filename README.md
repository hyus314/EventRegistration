# Overview
This is a web application that can be integrated in the process of registering events for cafes, restaurants and similar establishments.
A notification system via email is integrated.
The purpose of the web app is to help establishment owners to keep track of the registered events on a daily basis and keep their personnel informed about upcoming activities.
Thus, the usage of this application is a benefit in increasing productivity within the dynamic environment of such establishment.

# Tech stack
### &emsp; .NET 9  
The latest .NET version is integrated in order to have a smoother experience and to comply to latest development rules and trends.
.NET allows a wide range of .NET libraries such as the ones in this project. The project is built to be easily scalable and it is open for extension.

### &emsp; ASP.NET
This is an MVC Project that handles HTTP request and responses through dynamic UI/UX experience mixed with various technologies.
Each part of the web application is connected securely using the best programming practices.  
ASP.NET enables the separation between the database and the front-end part of the project.

### &emsp; EntityFramework & MSSQL 
Events are stored in an MSSQL database, with Entity Framework serving as the ORM mapper, which is the default for ASP.NET projects.
Modern EF ensures efficient queries to the project's database when LINQ is integrated within the scopes of the code. 
EF is essential when it comes to SQL attack prevention.

### &emsp; JavaScript 
Vanilla JS is integrated with _FullCalendar API_ for a seamless user experience.
Through DOM tree manipulation this technology provides an efficient connection to the server and maintains a simple and straight-forward user experience.

### &emsp; FullCalendar  
https://fullcalendar.io/docs
__This is an essential technology necessity for the project__
FullCalendar ensures the project has the intended UI/UX. FullCalendar has diverse functionalities that are flexible enough to be integrated within an ASP.NET MVC project.

### &emsp; MailJet
https://github.com/mailjet/mailjet-apiv3-dotnet
MailJet plays a crucial role in Event Registration’s notification system.
MailJet is included in the back-end of the project, where after each successful Database query, the automised system sends notifications in the forms of emails to all the workers and users.

# Security
### &emsp; DataProtection
Id's of entities are encrypted using .NET's DataProtection https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-9.0
for maximum security. Everytime a model or entity from the database is passed around to a different part of the project, this part of the web app is responsible for ensuring the processes of encrypting and decrypting crucial data such as id's.

### &emsp; Anti-Forgery Tokens 
Each HTTP request sent is encrypted using ASP .Net's Anti-Forgery system against CSRF attacks. 

# Features
### &emsp; ASP.NET Identity

Access to controllers is restricted, ensuring that only registered users can interact with the application.
Two roles exist: admins and regular workers. Admins can register, edit or delete workers.
Admins are registered in advance in the database by the owner of the project.
Users in both roles can add and edit events. Only admins can delete events.

### &emsp; Role-Based Permissions

| Action                 | Admin | Regular Worker |
|------------------------|:-----:|:-------------:|
| Register workers       | ✅    | ❌            |
| Edit workers          | ✅    | ❌            |
| Delete workers        | ✅    | ❌            |
| Add events            | ✅    | ✅            |
| Edit events           | ✅    | ✅            |
| Delete events         | ✅    | ❌            |

### &emsp; Notification email system
Upon creating, deleting or editing an event, all users registered in the system receive emails about new activity. 
Messages are defined through string constants and are seperated accordingly to other parts of the project.

### &emsp; FullCalendar JavaScript
Each day of the calendar is a different page that is loaded dynamically.
Once loaded, the user can choose the beginning hour of the event from a table that represents the working hours of the establishment.
Modals are loaded once the event's starting time is selected. Information about the event is then filled into the form and the HTTP post request is sent.
Events can be edited by clicking on an existing event on the working hours timetable of the establishment. 

### &emsp; Bootstrap Modals
Modals create a dynamic environment while using the web app. 

### &emsp; Available for all devices
The web app offers a responsive web design.

# Functionality screenshots
![Image](https://github.com/user-attachments/assets/9232a45b-c6c5-420f-a7ec-62bc24edb4e6)
![Image](https://github.com/user-attachments/assets/0f1ef45f-0cbb-4008-9544-f4208ab3ecf7)
![Image](https://github.com/user-attachments/assets/3c53f70f-b5f6-466a-a63f-6bd4cae5cd26)
![Image](https://github.com/user-attachments/assets/0fb15ad5-e8bf-469c-8f99-c3fc86625d54)
![Image](https://github.com/user-attachments/assets/33a63d1e-595b-4da6-bb27-6b84dc52801c)
![Image](https://github.com/user-attachments/assets/f4e91343-ccad-4c77-8d71-5fc903b5a8f7)
