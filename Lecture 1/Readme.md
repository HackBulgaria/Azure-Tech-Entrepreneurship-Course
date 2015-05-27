# Lecture 1: Intro
In this lecture we talked a bit about developing mobile-first SaaS in the cloud - why mobile first strategies are beneficial for you as a startup and why and when you should deliver SaaS. We also did a few demos of Azure PaaS offerings.

### Software tips
If you wish to replay some of the demos or fool around with Azure/VS, you can activate your 30-day Azure trial and download a community edition of VS using the following link: https://www.visualstudio.com/en-us/products/free-developer-offers-vs.aspx. Alternatively, if you're a student, you have access to Azure and Visual Studio through your dreamspark subscription.

### Demo highlights
##### Web App
The purpose of this demo was to show how easy it is to setup a Web App service in Azure, create an ASP.NET WebApi/MVC project and deploy it to Azure. To deploy the TechEntWebApp demo app to Azure, simply right click the project, choose Publish, and select Azure Web App.

##### Azure SQL Databases
The purpose of this demo was to highlight the Azure SQL Databases PaaS and to compare it to on-premise SQL Server installation. To connect to an Azure SQL Database, simply paste the connection string in the web.config. After that, you'll be able to execute REST calls against the api/Users endpoint.

##### Redis
The purpose of this demo was to show how easy it is to deploy an MVP url shortening service using redis. The substantial logic is located in ```TechEntWebApp/Helpers/RedisHelper.cs```. It demonstrates how we can utilize the ```StackExchange.Redis``` package to connect to a redis instance and store/retrieves values by key. The redirection logic is located in ```TechEntWebApp/Controllers/HomeController```.

##### Search
This is a demo of using Azure's Search PaaS to create and query an index. The core logic is located in ```SearchIndexPopulator/SearchHelper.cs```. It relies heavily on reflection and shows how to utilize Search's SDK to create an index, to populate it with some sample data, and finally to execute full-text search queries against it.