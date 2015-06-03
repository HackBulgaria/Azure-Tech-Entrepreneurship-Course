# Lecture 3: Microservices
In this lecture we talked about the pros and cons of using Microservices architecture.

We started with a brief overview of background processing and when you should use it (Hint: if response will take >2 secs, probably use background jobs). We discussed briefly how a background job service should behave and what background jobs should look like (idempotent/reentrant, with small arguments). We looked at [Hangfire](http://hangfire.io) as the leading .NET background job framework. Alternatives are [delayed_job](https://github.com/tobi/delayed_job) for Ruby, [Jesque](https://github.com/gresrun/jesque) for Java, [node-resque](https://github.com/taskrabbit/node-resque) for Node, etc.

After that we discussed why monolithic apps ultimately are a bad idea and how microservices resolve some of the most severe monolith drawbacks. We also mentioned how eventsourcing can fix the most serious problem with microservices - data consistency and synchronization.

Then we took a look at communication channels between services. Obviously Http is a possibility, but it adds too much overhead, so we looked first at [Redis](http://redis.io) and then at [RabbitMQ](http://www.rabbitmq.com). Both support the Publish/Subscribe pattern that works much like (old) TV/Radio does - there are services that broadcast messages without caring about who receives them and there are services that consume messages without caring who broadcast them. 

Unlike Redis, RabbitMQ supports the more robust queueing pattern where we have certain guarantees that only one worker will receive a message and that if it fails to process it, it will be requeued and sent to another.

### Demo highlights
We had demos for Background procesing, Redis, and RabbitMQ since demoing microservices would be too involved and not particularly educational. 

#### Background processing
In this demo, we started with a project that receives an image upload, resizes it to 5 different resolutions and stores it on disk for further queries. Then we could serve the image that is best suited for the current device's resolution. This is a common pattern for websites that serve a lot of images.

The upload process was not very optimal since we would receive the image and resize it within the same call - it took us ~30 secs to upload 5 images. To fix this, we used Hangfire to fire a background task that will eventually rescale the image and returned the 200 OK response immediately after that. To keep the background job arguments small, we stored the original to a temp location, that would then be picked up by the background job.

#### Redis
We created a simple demo that demonstrates a chat-like functionality. In the producer app, we subscribe to a "producer" channel and send messages to the "worker" subscribers, and in the worker app - vice versa. Thus producers can communicate with all workers and workers can communicate with all producers.

#### RabbitMQ
The first part of the demo demonstrated the queueing functionality of RabbitMQ - we created a queue and started sending messages to it. By using BasicQos, we were able to send messages to the next available worker instead of using a round-robin to blindly alternate between all available workers. To make it work, we had to manually acknowledge message processing after "the work" has been done.

The second part was designed to demonstrate the fanout (publish/subscribe) messaging similar to the redis demo.

### Additional reading
- [Hangfire's overview page](http://hangfire.io/overview.html), though Hangfire inclined, shows some of the basic functionality you should look for in a background processing framework.
- [Hangfire's best practices page](http://docs.hangfire.io/en/latest/best-practices.html) outlines best practices for background jobs for any framework, not just Hangfire.
- [Microservices.io](http://microservices.io) by Chris Richardson contains some great resources on microservices and what makes them a good architectural pattern.
- Luke Roberts's [blog post](http://blog.speak.io/how-we-use-microservices-and-event-sourcing-to-instantly-connect-calls/) for speak.io contains a real world example of the switch from monolith to microservices.
- A nice [SO answer](http://stackoverflow.com/a/1870045) that highlights why you should use a 3rd party queue solution instead of trying to roll your own :)