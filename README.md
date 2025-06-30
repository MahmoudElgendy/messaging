https://chatgpt.com/g/g-p-6832e54e2f8c81918718fd671b300e46-messaging/project

Aiming to build strong understand of 
- Http client
- Azure service bus
- Azure event hub
- rabbit mq => masstransit outboxing
- Kafka
- GRPC
  
main steps for creating Grpc service

1- create grpc project
2- add required packages
2- create .proto file
3- add reference in project file
4- build to generate code 
5- implement the service by inhere the generated base service
6- register the service using app.MapGet<YourService>();
