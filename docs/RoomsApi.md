# Org.OpenAPITools.Api.RoomsApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateRoom**](RoomsApi.md#createroom) | **POST** /locations/{locationId}/rooms | Create a Room.
[**DeleteRoom**](RoomsApi.md#deleteroom) | **DELETE** /locations/{locationId}/rooms/{roomId} | Delete a Room.
[**GetRoom**](RoomsApi.md#getroom) | **GET** /locations/{locationId}/rooms/{roomId} | Get a Room.
[**ListRooms**](RoomsApi.md#listrooms) | **GET** /locations/{locationId}/rooms | List Rooms.
[**UpdateRoom**](RoomsApi.md#updateroom) | **PUT** /locations/{locationId}/rooms/{roomId} | Update a Room.


<a name="createroom"></a>
# **CreateRoom**
> Room CreateRoom (string authorization, string locationId, CreateRoomRequest createRoomRequest)

Create a Room.

Create a Room for the Location. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateRoomExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RoomsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The ID of the location.
            var createRoomRequest = new CreateRoomRequest(); // CreateRoomRequest | 

            try
            {
                // Create a Room.
                Room result = apiInstance.CreateRoom(authorization, locationId, createRoomRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RoomsApi.CreateRoom: " + e.Message );
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
 **createRoomRequest** | [**CreateRoomRequest**](CreateRoomRequest.md)|  | 

### Return type

[**Room**](Room.md)

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

<a name="deleteroom"></a>
# **DeleteRoom**
> Object DeleteRoom (string authorization, string locationId, string roomId)

Delete a Room.

Delete a Room from a location.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteRoomExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RoomsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The ID of the location.
            var roomId = roomId_example;  // string | The ID of the room.

            try
            {
                // Delete a Room.
                Object result = apiInstance.DeleteRoom(authorization, locationId, roomId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RoomsApi.DeleteRoom: " + e.Message );
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
 **roomId** | **string**| The ID of the room. | 

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

<a name="getroom"></a>
# **GetRoom**
> Room GetRoom (string authorization, string locationId, string roomId)

Get a Room.

Get a specific Room.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetRoomExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RoomsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The ID of the location.
            var roomId = roomId_example;  // string | The ID of the room.

            try
            {
                // Get a Room.
                Room result = apiInstance.GetRoom(authorization, locationId, roomId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RoomsApi.GetRoom: " + e.Message );
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
 **roomId** | **string**| The ID of the room. | 

### Return type

[**Room**](Room.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A Room. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listrooms"></a>
# **ListRooms**
> PagedRooms ListRooms (string authorization, string locationId)

List Rooms.

List all Rooms currently available in a Location.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListRoomsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RoomsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The ID of the location.

            try
            {
                // List Rooms.
                PagedRooms result = apiInstance.ListRooms(authorization, locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RoomsApi.ListRooms: " + e.Message );
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

[**PagedRooms**](PagedRooms.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An array of Rooms |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateroom"></a>
# **UpdateRoom**
> Room UpdateRoom (string authorization, string locationId, string roomId, UpdateRoomRequest updateRoomRequest)

Update a Room.

All the fields in the request body are optional. Only the specified fields will be updated.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateRoomExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RoomsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The ID of the location.
            var roomId = roomId_example;  // string | The ID of the room.
            var updateRoomRequest = new UpdateRoomRequest(); // UpdateRoomRequest | 

            try
            {
                // Update a Room.
                Room result = apiInstance.UpdateRoom(authorization, locationId, roomId, updateRoomRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RoomsApi.UpdateRoom: " + e.Message );
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
 **roomId** | **string**| The ID of the room. | 
 **updateRoomRequest** | [**UpdateRoomRequest**](UpdateRoomRequest.md)|  | 

### Return type

[**Room**](Room.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A Room. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

