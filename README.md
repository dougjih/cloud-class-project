# Cloud Computing Final Project
50:198:541 Parallel, Distributed, Grid, and Cloud Computing with Ian Lebbern, Rutgers University-Camden, Spring 2021  
Doug Jih

## Objective
Design and implement a cloud application to demonstrate sound uses of cloud services.

## Application Description

The is a cloud web app that helps the user identify and track sports betting pairings for promotional arbitrage.
1. The user can enter candidate sports betting parings, each of which consists of opposite outcomes of the same event at two different casinos.
1. The app calculates the projected profits and losses for each outcome.
1. The app keeps persistent memory of the entries in cloud storage.
1. The app provides account creation and sign-in to allow multiple users to have separate and private books.
1. The app runs in a modern web browser and requires no explicit installation of software.

## Architecture Overview

This project uses the following cloud services:
- [Azure Static Web Apps](https://azure.microsoft.com/en-us/services/app-service/static/) - A Microsoft Azure PaaS web app service that hosts a static front end web page connected to a dynamic back end of APIs, with streamlined CI/CD integration with GitHub
- [Azure API Management](https://azure.microsoft.com/en-us/services/api-management/) - ?
- [Azure Functions](https://azure.microsoft.com/en-us/services/functions/)
- [Azure Active Directory](https://azure.microsoft.com/en-us/services/active-directory/)
	- [Azure Active Directory B2C](https://azure.microsoft.com/en-us/services/active-directory/external-identities/b2c/)
- [Azure Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/)
- [GitHub](https://github.com/)

This project uses the following software development tools:
- [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) - a Microsoft web framework that enables programming of single-page web apps using .NET and C#
- [Visual Studio](https://visualstudio.microsoft.com/) - a Microsoft integrated development environment (IDE)

## Discussions


### Challenges
The number of choices in cloud services is ***vast***. As a completely new cloud software developer, I had a paralysis by indecision and procrastination because of the uncertainty and doubts I felt. Pressure of the deadline eventually resolved the problem.

At first, I looked at using AWS with the "classroom" account, but I ended up using an Azure free trial account because certain aspects of an AWS classroom account (such as IAM) worked differently from a regular account, and I did not want to spend time figuring it out when tutorials did not work. Also, because I chose to use Blazor for my web app development because I had experiece with using .NET and C# for desktop software development; and because Blazor is a Microsoft product, and Azure offers a more integrated deployment experience.

Neverthess, I encountered a a lot of difficulties getting anything to work beyond the pre-made examples in tutorials. Getting different components to work together was hard for me. I am not sure why.


### Background & Motivation
Sports betting is legal in New Jersey. It is possible to make small profits with sports betting without relying on luck by making use of promotional offers and betting on both outcomes of a two-outcome event at different casinos.

Betting on both outcomes alone ensures a small loss because of house edge, but if the promotional offer's value is greater than the house edge, then doing so results in a (modest) profit. Spreading bets on both outcomes at different casinos is both necessary and desirable: It is necessary because one cannot bet both outcomes at the same casino while using its promotion without running afoul of the promotion rules, but doing so at two different casinos is fine. It is also desirable because different casinos usually offer different odds for the same bet, and shopping them helps to reduce the loss to house edge.

Manually finding candidate parings can be time consuming: Doing so involves manually searching through and comparing the many potential candidates. A software program should be able to automate some of the search process and save time in the long term.

However, automating searching requires web scraping of dynamic and irregular web site contents from the online casinos. I deem this too much a difficulty and opt to omit this aspect, and focus on basic functionality that I feel I can feasibly develop in time: taking manual entries, calculating projected profits and losses, and keeping the entries in cloud storage.

### References

### Overall

- [Build a Student Feedback Analyzer with Blazor, Azure Functions, Azure Static Web Apps and Azure Cognitive Services](https://dev.to/ashirwadsatapathi/build-a-student-feedback-analyzer-with-blazor-azure-functions-azure-static-web-apps-and-azure-cognitive-services-4acj)

### Azure Static Web Apps

- [Tutorial: Building a static web app with Blazor in Azure Static Web Apps](https://docs.microsoft.com/en-us/azure/static-web-apps/deploy-blazor)

####  Azure Active Directory

- [Azure AD B2C Quickstart with Visual Studio & Blazor](https://medium.com/marcus-tee-anytime/azure-ad-b2c-quickstart-with-visual-studio-blazor-563efdff6fdd)
- [Secure an ASP.NET Core Blazor WebAssembly standalone app with Azure Active Directory B2C](https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/standalone-with-azure-active-directory-b2c?view=aspnetcore-5.0)
- [Set up sign-up and sign-in with a Microsoft account using Azure Active Directory B2C](https://docs.microsoft.com/en-us/azure/active-directory-b2c/identity-provider-microsoft-account?pivots=b2c-user-flow)
