Author: Abhishek Zunjurwad.
This is a programming challenge received for an job opportunity.
This repository includes C# .NET solution to query a service and uses SpecFlow to test the response received back from service.
Files: 
1. HttpClient: Used to request load from the remote web service, Used JSON formatter to desearlize response
2. Contracts: Includes 2 classes to desearlize response
3. Features: Includes SPecFlow file containing a test case to test various aspects of the code
4. People.cs: Contains code to instantiate HttpClient and call the web service. In addition it also desearlizes data to contract classes and then generates sorted output as per requirement
5. Steps: This folder contains class file to implement test case written in feature file.
