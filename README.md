# SmartThingsNet - dotnet library for the SmartThings API

![.NET Core](https://github.com/daltskin/SmartThingsNet/workflows/.NET%20Core/badge.svg)![Github Package](https://img.shields.io/github/v/release/daltskin/smartthingsnet)![Nuget](https://img.shields.io/nuget/v/smartthingsnet)

# Overview

The SmartThings API supports [REST](https://en.wikipedia.org/wiki/Representational_state_transfer), resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1), and all responses are sent as [JSON](http://www.json.org/).

For an example on how to use this SDK, please refer to the [SmartThings Terminal](https://github.com/daltskin/SmartThingsTerminal) cli tool.

*Note: This SDK is mostly auto-generated from the public [SmartThings open-api definition](https://swagger.api.smartthings.com/public/st-api.yml) but with necessary compilation fixes.  
The underlying SmartThings API is currently in preview and has ongoing changes - there are some discrepancies between what functionality is exposed and what is implemented.  
Therefore if things don't work, please check the SmartThings API behaves in the way you expect first.*
