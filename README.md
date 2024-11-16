# AzureCredentialsDemo

This project demonstrates how to use Azure services with managed identity or Azure CLI credentials.

In this demo, I am using consuming an Azure OpenAI resource to generate images using the DALL-E model.

## Prerequisites

- .NET 8.0 SDK
- An Azure subscription with an Azure OpenAI resource created
- Permission set to the Azure OpenAI resource (here I am using "Cognitive Services OpenAI User")
- A GPT-3.5 turbo model deployed within this Azure OpenAI resource (this deployment is named "gpt-35-turbo" here in the code)
- A "dall-e 3" model deployed within this Azure OpenAI resource (this deployment is named "dall-e-3" here in the code)

## Running the Project

Don't forget to Connect to your Azure account (the one having the appropriate permission on the Azure OpenAI resource):
```sh
az login
```

To run the project, use the following command:
```sh
dotnet run
```

## Debug
You can debug the project using Visual Studio Code:
```sh
code .
```
Then, press F5 to start debugging.

## Additional information
To check which Azure account is currently connected, use the following command:
```sh
az account show
```