# SmartThingsNet.Api.LocationsApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateLocation**](LocationsApi.md#createlocation) | **POST** /locations | Create a Location.
[**DeleteLocation**](LocationsApi.md#deletelocation) | **DELETE** /locations/{locationId} | Delete a Location.
[**GetLocation**](LocationsApi.md#getlocation) | **GET** /locations/{locationId} | Get a Location.
[**ListLocations**](LocationsApi.md#listlocations) | **GET** /locations | List Locations.
[**UpdateLocation**](LocationsApi.md#updatelocation) | **PUT** /locations/{locationId} | Update a Location.


<a name="createlocation"></a>
# **CreateLocation**
> Location CreateLocation (CreateLocationRequest createLocationRequest)

Create a Location.

Create a Location for a user. We will try and create the Location geographically close to the country code provided in the request body. If we do not support Location creation in the requested country code, then the API will return a 422 error response with an error code of UnsupportedGeoRegionError. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class CreateLocationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new LocationsApi(config);

            var createLocationRequest = new CreateLocationRequest(); // CreateLocationRequest | 

            try
            {
                // Create a Location.
                Location result = apiInstance.CreateLocation(createLocationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationsApi.CreateLocation: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| OAuth token | 
 **createLocationRequest** | [**CreateLocationRequest**](CreateLocationRequest.md)|  | 

### Return type

[**Location**](Location.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Created successfully. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletelocation"></a>
# **DeleteLocation**
> Object DeleteLocation (string locationId)

Delete a Location.

Delete a Location from a user's account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class DeleteLocationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new LocationsApi(config);

            var locationId = locationId_example;  // string | The ID of the location.

            try
            {
                // Delete a Location.
                Object result = apiInstance.DeleteLocation(locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationsApi.DeleteLocation: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| OAuth token | 
 **locationId** | **string**| The ID of the location. | 

### Return type

**Object**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An empty object response. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getlocation"></a>
# **GetLocation**
> Location GetLocation (string locationId)

Get a Location.

Get a specific Location from a user's account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GetLocationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new LocationsApi(config);

            var locationId = locationId_example;  // string | The ID of the location.

            try
            {
                // Get a Location.
                Location result = apiInstance.GetLocation(locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationsApi.GetLocation: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| OAuth token | 
 **locationId** | **string**| The ID of the location. | 

### Return type

[**Location**](Location.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A Location. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listlocations"></a>
# **ListLocations**
> PagedLocations ListLocations (string authorization)

List Locations.

List all Locations currently available in a user account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class ListLocationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new LocationsApi(config);


            try
            {
                // List Locations.
                PagedLocations result = apiInstance.ListLocations(authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationsApi.ListLocations: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| OAuth token | 

### Return type

[**PagedLocations**](PagedLocations.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An array of Locations |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatelocation"></a>
# **UpdateLocation**
> Location UpdateLocation (string locationId, UpdateLocationRequest updateLocationRequest)

Update a Location.

All the fields in the request body are optional. Only the specified fields will be updated.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class UpdateLocationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new LocationsApi(config);

            var locationId = locationId_example;  // string | The ID of the location.
            var updateLocationRequest = new UpdateLocationRequest(); // UpdateLocationRequest | 

            try
            {
                // Update a Location.
                Location result = apiInstance.UpdateLocation(locationId, updateLocationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationsApi.UpdateLocation: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| OAuth token | 
 **locationId** | **string**| The ID of the location. | 
 **updateLocationRequest** | [**UpdateLocationRequest**](UpdateLocationRequest.md)|  | 

### Return type

[**Location**](Location.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A Location. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

