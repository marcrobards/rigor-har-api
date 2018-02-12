# rigor-har-api

## Overview

A REST API for storing and analyzing HAR files.

There is a container object to encapsulate the HAR file data:

    {
        "harFileId": {"type": "number"},
        "url" : {"type": "string"},
        "startedDateTime" : {"type": "date-time" }
        "jsonContent" : {"type": "object"}
    }

CRUD operations follow standard REST rules:

### GET

    api/harfiles/{id}

GET will return a 404 error if {id} is not found.

### POST

    api/harfiles

POST performs manual valiation.

### PUT

    api/harfiles/{id}

PUT performs model-based validation and will return a list of errors if validaiton fails. PUT returns a 404 error if {id} is not found.

### DELETE

    api/harfiles{id}

## Helper Functions

### GET Content

    api/harfiles/1/content

GET Content will return the HAR file data only without the container object for the specified {id} or a 404 error if {id} is not found.

### GET All

    api/harfiles

GET All returns all stored HAR files.

### GET Blocked

    api/harfiles/{id}/blocked

GET Blocked will return a list of Entries ordered by blocked timing descending.

### GET BodySize

    api/harfiles/{id}/bodysize

GET BodySize will return an object with the average and total bodySize across all requests:

    {
        "averageBodySize": {"type": "number"},
        "totalBodySize": {"type": "number"}
    }

### GET Response URLs by Filter

    api/harfiles/{id}/responseurl/{filter}

GET Response URLs by Filter will return a list of all request URLs that contain {filter}.

The file used for testing is Rigor.HAR.API.Tests/www.microsoft.com.har, taken from [https://www.microsoft.com/net/](https://www.microsoft.com/net/, ".NET")