# Lecture 4: Mobile development with Xamarin
In this lecture we talked about the different approaches to mobile development. Roughly, we've categorized them in 3 categories - *Native*, *X-Platform*, and *Hybrid* (HTML/JS/CSS).

The **Native** approach produces a fast application that looks consistent with the user's operating system paradigms and allows you to easily integrate with 3rd party libraries and tools. It, however, requires more time to develop and more experienced developers. Examples: Objective C/Swift for iOS, Java/C++ for Android, C# for Windows Phone.

The **Cross Platform** approach allows code sharing of the Models/ViewModels and application logic, but the UI is done separately for each platform. This produces native-looking apps that perform just as fast, but presents some challenges when integrating with 3rd party libraries if there are no bindings (mapping between native classes/methods and the x-platform equivalent) for them. It also requires some knowledge of the native platform to be able to efficiently develop. Examples: [Xamarin](https://xamarin.com), [J2ObjC](https://github.com/google/j2objc), [RoboVM](http://robovm.com), [NativeScript](https://www.nativescript.org).

The **Hybrid** approach enables fast iterations and virtually 100% code reshare. The downside is that the app doesn't look consistent with what the user would expect, performs slower, and has some limitations. Ultimately it works best in the early stages of a startup when it is more important to validate an idea than to have a truly polished UI/UX. Exampls: [PhoneGap](http://phonegap.com), [Cordova](https://cordova.apache.org), [Telerik AppBuilder](http://www.telerik.com/appbuilder).

### Demo highlights
We did a rather long demo highlighting some key aspects of Xamarin development. The purpose was to demonstrate how to build a basic chat application similar to Slack.

#### Chat backend
Our backend project exposes methods to get/create channels and messages by proxying the calls to Redis. It's fairly naive implementation, but works fine.

#### Chat client
This is somewhat complex demo of how Xamarin projects are structured and decoupled using dependency injection, and partial classes. Ultimately, the goal is to have a shared project that contains platform agnostic logic, and platform specific projects for the platform-specific logic. 

### Additional reading
- [Mobile App Performance Redux](https://medium.com/@harrycheung/mobile-app-performance-redux-e512be94f976) is a performance comparison of popular cross platform tools in a computationally intensive scenario.
- [App performance is a major differentiator of Top 10 cross platform tools](http://research2guidance.com/app-performance-is-a-major-differentiator-of-top-10-cross-platform-tools/) takes a more statistical approach by interviewing multiple devs about their experiences.
- [Doubts About Cross-Platform Mobile Development](https://adtmag.com/blogs/dev-watch/2014/08/crossplatform-development-doubts.aspx) shows awareness comparison of popular cross platform tools.
- [The Xamarin Component Store](https://components.xamarin.com) list some of the high quality components that you can integrate directly in your app.
- [The Monotouch Bindings repo](https://github.com/mono/monotouch-bindings) is a collection of open source bindings for 3rd party iOS libraries. It contains only a small portion of the bindings available on Github.