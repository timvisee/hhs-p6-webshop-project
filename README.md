[![Build status on Circle CI in production branch](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project/production.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project/tree/production)
[![Build status on Circle CI in master branch](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project/master.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project/tree/master)
[![Build status on Circle CI in last commit](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)

# HHS P6 Webshop Project
C#/ASP.NET webshop collaboration project for college (HHS) period 6.

## Requirements
This project is built against .NET Core. Therefore, it is required to install .NET Core which can be found on [this](https://www.microsoft.com/net/core) page.
Required packages might be installed automatically, if not, the command `dotnet restore` can be used.

Additional notes for building:
* On Windows it is recommended to install [IIS](https://www.microsoft.com/en-us/download/details.aspx?id=48264).
* The database should be created/migrated when using the application for the first time,
  or when the database structure was updated in an update. Use one of the following commands:
    * Database reset command: `dotnet run -- --db-init --db-init-exit` _(Recommended)_
    * Database migration command: `dotnet ef database update`

## Commandline Options
The following command line arguments are supported:
* `--db-init`: Initialize the database on startup, this will reset and override any existing database.
* `--db-init-exit`: Exits the application after the database has been initialized.

## Build
The project is currently being built automatically using the CI services in the table below.

|Platform|Branch|Build Status|Service||
|:---|:---:|:---:|:---|---|
|Linux|production|[![Build status on Circle CI](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project/production.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|CircleCI|[View Status (private)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project/tree/production)|
|Linux|master|[![Build status on Circle CI](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project/master.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|CircleCI|[View Status (private)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project/tree/master)|
|Linux|last commit|[![Build status on Circle CI](https://img.shields.io/circleci/token/b86ed6918f78b8ae37292aabbcd0afcd381ff7a9/project/github/timvisee/hhs-p6-webshop-project.svg)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|CircleCI|[View Status (private)](https://circleci.com/gh/timvisee/hhs-p6-webshop-project)|
