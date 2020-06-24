# Org.OpenAPITools.Api.DeviceprofilesApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateDeviceProfile**](DeviceprofilesApi.md#createdeviceprofile) | **POST** /deviceprofiles | Create a device profile
[**DeleteDeviceProfile**](DeviceprofilesApi.md#deletedeviceprofile) | **DELETE** /deviceprofiles/{deviceProfileId} | Delete a device profile
[**GetDeviceProfile**](DeviceprofilesApi.md#getdeviceprofile) | **GET** /deviceprofiles/{deviceProfileId} | GET a device profile
[**ListDeviceProfiles**](DeviceprofilesApi.md#listdeviceprofiles) | **GET** /deviceprofiles | List all device profiles for the authenticated user
[**UpdateDeviceProfile**](DeviceprofilesApi.md#updatedeviceprofile) | **PUT** /deviceprofiles/{deviceProfileId} | Update a device profile.


<a name="createdeviceprofile"></a>
# **CreateDeviceProfile**
> DeviceProfile CreateDeviceProfile (string authorization, CreateDeviceProfileRequest request = null)

Create a device profile

Create a device profile.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDeviceProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DeviceprofilesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var request = new CreateDeviceProfileRequest(); // CreateDeviceProfileRequest | The device profile to be created. (optional) 

            try
            {
                // Create a device profile
                DeviceProfile result = apiInstance.CreateDeviceProfile(authorization, request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceprofilesApi.CreateDeviceProfile: " + e.Message );
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
 **request** | [**CreateDeviceProfileRequest**](CreateDeviceProfileRequest.md)| The device profile to be created. | [optional] 

### Return type

[**DeviceProfile**](DeviceProfile.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The device profile. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletedeviceprofile"></a>
# **DeleteDeviceProfile**
> Object DeleteDeviceProfile (string authorization, string deviceProfileId)

Delete a device profile

Delete a device profile by ID. Admin use only.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDeviceProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DeviceprofilesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceProfileId = deviceProfileId_example;  // string | the device profile ID

            try
            {
                // Delete a device profile
                Object result = apiInstance.DeleteDeviceProfile(authorization, deviceProfileId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceprofilesApi.DeleteDeviceProfile: " + e.Message );
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
 **deviceProfileId** | **string**| the device profile ID | 

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
| **200** | Device profile deleted. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getdeviceprofile"></a>
# **GetDeviceProfile**
> DeviceProfile GetDeviceProfile (string authorization, string deviceProfileId, string acceptLanguage = null)

GET a device profile

GET a device profile.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDeviceProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DeviceprofilesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceProfileId = deviceProfileId_example;  // string | the device profile ID
            var acceptLanguage = acceptLanguage_example;  // string | Language header representing the client's preferred language. The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional) 

            try
            {
                // GET a device profile
                DeviceProfile result = apiInstance.GetDeviceProfile(authorization, deviceProfileId, acceptLanguage);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceprofilesApi.GetDeviceProfile: " + e.Message );
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
 **deviceProfileId** | **string**| the device profile ID | 
 **acceptLanguage** | **string**| Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) | [optional] 

### Return type

[**DeviceProfile**](DeviceProfile.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A Device Profile |  * Content-Language - This header field describes the natural language(s) of the intended audience for the representation. This can have multiple values as per [RFC 7231, section 3.1.3.2](https://tools.ietf.org/html/rfc7231#section-3.1.3.2) <br>  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listdeviceprofiles"></a>
# **ListDeviceProfiles**
> PagedDeviceProfiles ListDeviceProfiles (string authorization, List<string> profileId = null)

List all device profiles for the authenticated user

List device profiles.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDeviceProfilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DeviceprofilesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var profileId = new List<string>(); // List<string> | The device profiles IDs to filter the results by.  (optional) 

            try
            {
                // List all device profiles for the authenticated user
                PagedDeviceProfiles result = apiInstance.ListDeviceProfiles(authorization, profileId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceprofilesApi.ListDeviceProfiles: " + e.Message );
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
 **profileId** | [**List&lt;string&gt;**](string.md)| The device profiles IDs to filter the results by.  | [optional] 

### Return type

[**PagedDeviceProfiles**](PagedDeviceProfiles.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A list of the users device profiles. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatedeviceprofile"></a>
# **UpdateDeviceProfile**
> DeviceProfile UpdateDeviceProfile (string authorization, string deviceProfileId, UpdateDeviceProfileRequest request = null)

Update a device profile.

Update a device profile. The device profile has to be in development status

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDeviceProfileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DeviceprofilesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceProfileId = deviceProfileId_example;  // string | the device profile ID
            var request = new UpdateDeviceProfileRequest(); // UpdateDeviceProfileRequest | The device profile to be updated. (optional) 

            try
            {
                // Update a device profile.
                DeviceProfile result = apiInstance.UpdateDeviceProfile(authorization, deviceProfileId, request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DeviceprofilesApi.UpdateDeviceProfile: " + e.Message );
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
 **deviceProfileId** | **string**| the device profile ID | 
 **request** | [**UpdateDeviceProfileRequest**](UpdateDeviceProfileRequest.md)| The device profile to be updated. | [optional] 

### Return type

[**DeviceProfile**](DeviceProfile.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The device profile |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

