## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# Collection Calendar API

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/das-collection-calendar?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=das-collection-calendar&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=_projectId_&metric=alert_status)](https://sonarcloud.io/dashboard?id=_projectId_)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/jira/software/c/projects/FLP/boards/753)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/NDL/pages/3480354918/Flexible+Payments+Models)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

The Collection Calendar API provides the latest collection calendar information and should be used by all applications within the Apprenticeship Service which need collection calendar information rather than maintaining their own copies.

## How It Works

The Inner API/Application repository is made up of a simple Web API and SQL database.

The API provides a number of GET endpoints responsible for querying the collection calendar.  No functionality exists to update the calendar data.

## 🚀 Installation

### Pre-Requisites

* A clone of this repository
* A code editor that supports .Net8
* Azure Storage Emulator (Azureite)
* A SQL Server instance

### Config

Most of the application configuration is taken from the [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config) and the default values can be used in most cases.  The config json will need to be added to the local Azure Storage instance with a a PartitionKey of LOCAL and a RowKey of SFA.DAS.CollectionCalendar_1.0. To run the application locally the following values need to be updated:

| Name                        | Value                                    |
| --------------------------- | ---------------------------------------- |
| DbConnectionString          | Your local DB instance connection string |

## 🔗 External Dependencies

None

## Running Locally

* Deploy the database project to the database instance specified in config
* Make sure Azure Storage Emulator (Azureite) is running
* Run the application