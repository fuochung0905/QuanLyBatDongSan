FROM openjdk:17-jdk-slim
WORKDIR /app
COPY /build/libs/microservice-0.0.1-SNAPSHOT.jar /app/microservice.jar
EXPOSE 8080

ENTRYPOINT ["java", "-jar", "microservice.jar"]