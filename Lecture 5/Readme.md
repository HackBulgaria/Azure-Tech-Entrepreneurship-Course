# Lecture 5: Startup infrastructure
In this lecture we discussed different tools to improve productivity of our startup team.

### Source control
Source control is mostly personal preference and, sometimes, matter of client choice (e.g. if you are developing a solution for a corporate client, they might insist that you use their source control solution). Currently, Git is considered the most popular version control system mostly due to its distributed nature.

For hosting, the two major Git players are [GitHub](https://github.com) and [Bitbucket](https://bitbucket.org). The major difference (apart from visual) is the pricing model - GitHub are charging per private repo, whereas Bitbucket are charging per contributor. Pricing is fairly insignificant though, since it is negligible compared to a developer's salary, so choose whatever looks better to you :)

### Continous integration with Jenkins
[Jenkins](https://jenkins-ci.org) is an excellent continous integration service that allows you to easily setup jobs to build, test, and deploy your projects.

If you are using it in conjunction with Git/GitHub, I would recommend that you use the [Pull request builder plugin](https://wiki.jenkins-ci.org/display/JENKINS/GitHub+pull+request+builder+plugin) - it will detect your pull requests, merge them locally, execute a build, run the tests and let you know if it's safe for merging or not. The modular nature of Jenkins makes it great for most repetitive tasks, so I strongly urge you to setup a CI service as early as possible.

Alternatives to Jenkins are [Travis CI](https://travis-ci.org) and [AppVeyor](http://www.appveyor.com), though the latter is .NET focused.

### Domain and Email
For buying your domain, use a well-known registrar like GoDaddy - it will save you a lot of time, since most services that require DNS manipulations (Email, domain verification, etc.) have step by step instructions for it. A good tool to find available domains is [name|grep](http://namegrep.com) - it allows using RegEx, but is limited to .com, .net, and .org top level domains. Since securing a good domain for your startup is extremely hard, try to be creative and see if you can utilize some of the country-specific top-level domains. For example, "shake.it", "where.to", and so on.

For email, we use [Zoho](https://www.zoho.com) which provides up to 25 mailboxes for free (10 initial and 15 through referrals). There are other solutions out there, but we're satisfied with it.

### Communication
Try to setup a work-only communication channel as soon as possible. If you use Skype, have a work-only Skype name, if you use other service, make sure it has native apps for the platforms you are using and you don't need to open a browser to reply to notifications. Don't use Facebook. We use [Slack](https://slack.com) and are very happy with it - it offers great integration with other services (e.g. GitHub, Jenkins) and fairly well designed native apps.

### Task management
Find a solution that is lightweight, somewhat freestyle, but that enforces some structure. We are utilizing [GitHub issues](https://guides.github.com/features/issues/) and are very happy with it. We set milestones for app releases, rely heavily on Pull Requests and everything is integrated nicely with the rest of GitHub.

We tried using [Trello](https://trello.com), but didn't like it all that much because it was too freestyle and relied on users to organize information, and people are generally bad at this. Still, there are many companies that use it and seem to like it.

If you are corporate minded, you could go for [Jira](https://jira.atlassian.com) or [TFS work items](https://msdn.microsoft.com/en-us/library/ms181236.aspx) but we feel that those are too process oriented.

Finally, if you are using different tools, you could use [Zapier](https://zapier.com) to integrate those. It is a service that allows you to define triggers in one service and work to be done in another service based on those triggers. During the lecture we did a brief demo on how to create a Trello card when a GitHub issue is created.