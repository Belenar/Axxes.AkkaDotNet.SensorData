# Axxes.AkkaDotNet.SensorData

Welcome to the code samples repository for my 'Drinking a river of IoT data with Akka.NET' talk.

## Slides

You can find all slides for my talk in the slides folder.

## What to expect

The sources in this repository holds 2 command line applications:
- **AkkaDotNet.SensorData.ActorSystemHost**: An application with a sample ActorSystem that processes Meter readings and generates alerts when certain thresholds are exceeded.
- **AkkaDotNet.SensorData.MessageGenerator**: An application that generates fake data, as if it came from real life sensors, and sends it to the 'ActorSystemHost' via Akka.Remote.

## How to get started

1. Clone this repo
2. Create 2 SQL databases. I used 'AkkaPersistence' and 'SensorData' on the localDB instance
3. Run the SQL script found in scripts/CreateSensorDataDB.sql on the DB you plan to use for sensor data and alert configurations.
4. Adjust Connectionstrings in:
    - src/AkkaDotNet.SensorData.ActorSystemHost/appsettings.json (SensorData)
    - src/AkkaDotNet.SensorData.ActorSystemHost/akka.conf (AkkaPersistence, 2x)
5. Set  both Console applications as Startup projects
6. Run the code
7. Type 'start' in the message generator 
8. Place some breakpoints, see what happens.

## Known issues
Since the message generator starts from a random number, you will need to clear both database if you restart it, or you will end up with bogus data.