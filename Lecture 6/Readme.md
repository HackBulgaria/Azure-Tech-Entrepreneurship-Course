# Lecture 6: Integration with other services
In this lecture we discussed different services that we could easily integrate with our app to boost UX, our knowlege, or bank accounts :)

### Payments - [Braintree](https://www.braintreepayments.com)
We demonstrated how to integrate with Braintree's service. The flow is as follows:

1. The server requests a payment token from Braintree and sends it to the client.
2. The client initializes the Braintree UI using that token. It may or may not contain reference to a user stored in the Braintree vault. If it does, previous payment methods are suggested to the user.
3. Upon completion of payment, the Braintre SDK generates a payment nonce (another token), which is then sent to the server for validation.
4. The server consumes the nonce to request a payment from Braintree
5. Profit :)

Since Braintree are doing most of the heavy-lifting, integrating with it is fairly simple and ensures PCI compliance. They offer SDKs for most modern languages with Drop-in UI libraries, so integration takes a few hours.

### Feedback - [Uservoice](http://uservoice.com)
Uservoice is centralized helpdesk SaaS solution. It provides multiple benefits over using the simple email approach.
1. It offers instant answers based on fuzzy matching user queries with knowledge base articles (FAQ).
2. It provides users with a platform to vote on their most desired features.
3. Naturally, it has a system for managing support requests, comlete with notes, history, and so on. 
4. It offers native SDKs for most platforms, which make integration 4-rows-of-code process.

### Analytics
Analytics are important to setup from day-1, because they offer invaluable insight regarding users' activities and usage patterns. There are different offerings that are great for different tasks, and we discussed the following:

#### [Leanplum](http://leanplum.com)
Leanplum is a great tool that offers advanced mobile analytics along with A/B testing framework and marketing automation tools.

#### [Xamarin Insights](http://insights.xamarin.com)
A free Xamarin-focused analytics solution that excels at error reporting. We use it as an alternative to Crashlytics and are quite happy with it. I recommend to setup crash/error reporting solution ASAP to have a better overview of what's bugging your users.

#### [TapeRecorder](http://taperecorder.io)
Taperecorder is LaunHub-backed alternative to [Appsee](https://www.appsee.com). It allows you to capture videos of user interaction with your app. While it is extremely helpful to watch videos of user's activity in the early days, it has several drawbacks. For one, it doesn't scale well :) Watching a few videos a day is fine, but when you produce thousands, they become pointless. More importantly, it is rather invasive and people would be fairly unhappy if they found out.

### Monitoring
While analytics is great for understanding usage patterns, monitoring is crucial for understanding your backend architecture. During the lecture, we demoed [Uptime robot](http://uptimerobot.com) - a simple solution that can execute simple HTTP GETs or pings to ensure that your service is up and running. The free tier offers 5-minute period, and there are paid plans for more frequent updates.

A more advanced solution is [NewRelic](http://newrelic.com). It provides daemons for most operating systems and most languages that just work. Once the daemon is installed, it will send transaction data to the NewRelic portal. With free plans, you get a high-level overview of transaction traces, along with timing information. E.g. it can tell you that your .NET code took 120 ms to execute, while your ```SELECT * FROM Foo``` query took 860 ms.

### Facebook Login
Finally, we did a quick Facebook Login demo. It showed how to easily authenticate users and query Facebook for details. Using an OAuth provider instead of rolling your own signup greatly increases signup chances (since users hate to sign up) and decreases your work (regarding password storage, reset links, etc.).