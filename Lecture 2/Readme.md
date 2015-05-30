# Lecture 2: Intro
In this lecture we talked about selecting appropriate stack for a startup. We emphasized that it's important that you're comfortable with the stack and that it works for whatever business needs you might have. Additionally, mature and homogenous stacks are to be prefferred.

We looked into REST and DI/IoC and why and when we should use them (hint: almost always :)) Finally, we discussed OData and its pros/cons.

### Demo highlights
We only had one demo that emphasized the major points in the theoretical part of the lecture. The high-level purpose was to create a very raw Twitter clone.

#### Setting up DI/IoC (using [MEF 2](https://mef.codeplex.com))
Nothing too fancy here - add reference to Microsoft.Composition via NuGet. In global.asax.cs, we created a MEF container. What's not so intuitive is setting up MEF as the container for WebApi controllers. That's done via StandaloneContainer.cs - a not very interesting class that basically implements IDependencyResolver (which is used by WebApi to resolve controllers) and binds the lifetime of the controllers to the lifetime of the HttpRequest.

#### Setting up EF and Models
In our limited example, we wanted to have Users, Tweets, Retweets, and the ability to follow other users. Since we wanted to reuse the models, they are located in the Common assembly. Then we added a TwitterContext and wired it with EF. To create the tables and seed them with data, we enabled code first migration and created an initial migration.

#### Setting up OData
To enable OData, we added a reference to Microsoft.AspNet.OData via NuGet. Then, we need to create the OData model and register the OData routes with WebApi (```GetModel``` and ```config.MapODataServiceRoute``` in WebApiConfig). Then we used the built in CodeGen to generate scaffolding for UsersController. Since there was too much boilerplate, we cleaned it up and abstracted a generic base controller class that would make it much easier to implement new controllers. Then we exposed a Tweets controller with a few rows of code.

**NOTE**: Be careful which namespace you use for you OData controllers. v3 controllers are located in System.Web.Http.OData and v4 controllers are located in System.Web.OData. They are not compatible :)

#### Setting up Authentication
Finally, we set up authentication by using a dummy encrypt/decrypt methods in AuthenticationHelper. 

First, we ignore Email and Password from the OData model, making it impossible for OData consumers to retrieve them. 

Then, we created an AuthenticationResponse class that would be returned from the Login call. To map from a User to AuthenticationResponse, we used AutoMapper - a great tool I strongly recommend and we instructed it that the ```Token``` property would be mapped by encrypting the ```User.Id```. 

To be able to call the Login action(aka HttpPost), it needs to be registered in WebApiConfig and a namespace must be specified for actions. The Login endpoint then looks like ```http://localhost/TwitterApi/api/Users/TwitterApi.Login```. Additionally, we need to specify  exactly what parameters the Action accepts.

To retrieve the currently logged in user, they should send the token in the AuthorizationHeader with a Token scheme. The ```TwitterAuthorizationFilter``` reads the header and creates a ```GenericPrincipal``` that holds information about the currently logged in user.

To demonstrate that it all works, we created a ```Me``` function (aka HttpGet) that again had to be registered in WebApiConfig. It retrieves the context user and casts it to ```GenericIdentity``` and if it succeeds, queries the database for a user with that Id.

#### Consuming the service with Simple.OData
To consume the service, we created a small console app and added reference to Simple.OData. Then we could query the resources using LINQ-like syntax.

#### vNext
We can easily extend the current demo and make it more realistic. For example:
- [ ] We can clean up the Database schema - add a few constraints (Unique Email, 140 symbols for Tweet.Text, etc.)
- [ ] We can add Follow action to the Users controller and Retweet action to the Tweets controller.
- [ ] We can replace the current encryption with proper one.
- [ ] Etc, etc.