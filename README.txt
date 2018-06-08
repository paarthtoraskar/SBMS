Here are some pertinent points -

- This web app is a business management app for a small, fictional software application development company.
- This was built in Spring 2013, my first semester in grad school, as a credit requirement for a course and was my first foray into ASP.NET MVC. The point was to try out several of its features and get a good working knowledge. As a consequence, there are parts of this project that do not fit into the whole product or are incomplete. Recently, I've made some key updates.
- This is an ASP.NET MVC solution targeting .NET 4.5 built using the VS2012 IDE.
- There is an ASP.NET Web API project that handles the server side functionality of the file handler mentioned below.
- There is also a small Windows WPF project that handles the client side of that file handler.
- The three roles - public, emp, admin - determine what features are available to the corresponding users - general public, employees, and administrators respectively.
- There are currently three users registered for demo purposes - "public", "emp", and "admin" - all with the password "password".

**The features that are not self-explanatory are described below -
-File Handler
A small native Windows client is downloaded to the users computer.
This client interacts with the Web API to handle multi-file upload and single-file download of relevant files that can then be consumed by the main application.
For instance, the "ProductData.xml" file in the App_Data folder of the main application is what the Catalog web page sources its display items from.
-Give Employee Role
Moves a user from the public role to the employee role, thereby making the employee-only features available to the said user.
-Catalog and Cart
Facilitate sale of listed fictional products
-Contract
Facilitates user proposal for custom applications
-Employee Portal Menu
Reveals features only available to users with the "emp" or "admin" roles
-Admin Menu
Reveals features only available to users with the "admin" role
-Projects
Once a proposed Contract is approved by an admin, it is auto-added to the Projects pile
-Customers
Once a users proposed Contract is approved by an admin, (s)he is auto-added to the Customers list

**Known issues (To-do?) -
- Although roles have been defined, pages do not currently authenticate users against their roles. Having a direct link to a page makes the whole idea of roles pointless. :(
- The Contract and Project models (and other models) are barebone placeholders - certainly not exhaustive (or useful!).
- External authentication using OpenId (OAuth?) can (should!) easily be added.
- Project Issues, Project Milestones and Customer Issues features need filtering based on Project and Customer items.
- Project items have no pointers back to their original Contract items.