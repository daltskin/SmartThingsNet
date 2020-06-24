# Org.OpenAPITools.Api.ScenesApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ExecuteScene**](ScenesApi.md#executescene) | **POST** /scenes/{sceneId}/execute | Execute Scene
[**ListScenes**](ScenesApi.md#listscenes) | **GET** /scenes | List Scenes


<a name="executescene"></a>
# **ExecuteScene**
> StandardSuccessResponse ExecuteScene (string authorization, string sceneId, string locationId = null)

Execute Scene

Execute a Scene by id for the logged in user and given locationId

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ExecuteSceneExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ScenesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var sceneId = sceneId_example;  // string | The ID of the Scene.
            var locationId = locationId_example;  // string | The location of a scene. (optional) 

            try
            {
                // Execute Scene
                StandardSuccessResponse result = apiInstance.ExecuteScene(authorization, sceneId, locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenesApi.ExecuteScene: " + e.Message );
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
 **sceneId** | **string**| The ID of the Scene. | 
 **locationId** | **string**| The location of a scene. | [optional] 

### Return type

[**StandardSuccessResponse**](StandardSuccessResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The Scene has been executed |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listscenes"></a>
# **ListScenes**
> ScenePagedResult ListScenes (string authorization, string locationId = null)

List Scenes

Fetch a list of Scenes for the logged in user filtered by locationIds. If no locationId is sent, return scenes for all available locations

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListScenesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ScenesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The location of a scene. (optional) 

            try
            {
                // List Scenes
                ScenePagedResult result = apiInstance.ListScenes(authorization, locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenesApi.ListScenes: " + e.Message );
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
 **locationId** | **string**| The location of a scene. | [optional] 

### Return type

[**ScenePagedResult**](ScenePagedResult.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/vnd.smartthings+json, application/json, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The Scenes have been fetched |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

