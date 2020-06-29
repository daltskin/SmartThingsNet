# SmartThingsNet.Api.RulesApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateRule**](RulesApi.md#createrule) | **POST** /rules | Create a rule
[**DeleteRule**](RulesApi.md#deleterule) | **DELETE** /rules/{ruleId} | Delete a rule
[**ExecuteRule**](RulesApi.md#executerule) | **POST** /rules/execute/{ruleId} | Execute a rule
[**GetRule**](RulesApi.md#getrule) | **GET** /rules/{ruleId} | Get a Rule
[**ListRules**](RulesApi.md#listrules) | **GET** /rules | Rules list
[**UpdateRule**](RulesApi.md#updaterule) | **PUT** /rules/{ruleId} | Update a rule


<a name="createrule"></a>
# **CreateRule**
> Object CreateRule (string locationId, RuleRequest request)

Create a rule

Create a rule for the location and token principal

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class CreateRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RulesApi(config);

            var locationId = locationId_example;  // string | The ID of the location in which to create the rule in.
            var request = new RuleRequest(); // RuleRequest | The rule to be created.

            try
            {
                // Create a rule
                Object result = apiInstance.CreateRule(locationId, request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.CreateRule: " + e.Message );
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
 **locationId** | **string**| The ID of the location in which to create the rule in. | 
 **request** | [**RuleRequest**](RuleRequest.md)| The rule to be created. | 

### Return type

**Object**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The created rule |  -  |
| **401** | Not authenticated |  -  |
| **403** | Not authorized |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleterule"></a>
# **DeleteRule**
> Rule DeleteRule (string ruleId, string locationId)

Delete a rule

Delete a rule

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class DeleteRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RulesApi(config);

            var ruleId = ruleId_example;  // string | The rule ID
            var locationId = locationId_example;  // string | The ID of the location in which to delete the rule in.

            try
            {
                // Delete a rule
                Rule result = apiInstance.DeleteRule(ruleId, locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.DeleteRule: " + e.Message );
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
 **ruleId** | **string**| The rule ID | 
 **locationId** | **string**| The ID of the location in which to delete the rule in. | 

### Return type

[**Rule**](Rule.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully deleted |  -  |
| **401** | Not authenticated |  -  |
| **403** | Not authorized or not found |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="executerule"></a>
# **ExecuteRule**
> RuleExecutionResponse ExecuteRule (string ruleId, string locationId)

Execute a rule

Trigger Rule execution given a rule ID

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class ExecuteRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RulesApi(config);

            var ruleId = ruleId_example;  // string | The rule ID
            var locationId = locationId_example;  // string | The ID of the location that both the installed smart app and source are associated with.

            try
            {
                // Execute a rule
                RuleExecutionResponse result = apiInstance.ExecuteRule(ruleId, locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.ExecuteRule: " + e.Message );
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
 **ruleId** | **string**| The rule ID | 
 **locationId** | **string**| The ID of the location that both the installed smart app and source are associated with. | 

### Return type

[**RuleExecutionResponse**](RuleExecutionResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully Executed |  -  |
| **401** | Not authenticated |  -  |
| **403** | Not authorized or not found |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getrule"></a>
# **GetRule**
> Rule GetRule (string ruleId, string locationId)

Get a Rule

Get a rule

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GetRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RulesApi(config);

            var ruleId = ruleId_example;  // string | The rule ID
            var locationId = locationId_example;  // string | The ID of the location to list the rules for.

            try
            {
                // Get a Rule
                Rule result = apiInstance.GetRule(ruleId, locationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.GetRule: " + e.Message );
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
 **ruleId** | **string**| The rule ID | 
 **locationId** | **string**| The ID of the location to list the rules for. | 

### Return type

[**Rule**](Rule.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The rule |  -  |
| **401** | Not authenticated |  -  |
| **403** | Not authorized or not found |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listrules"></a>
# **ListRules**
> PagedRules ListRules (string locationId, int? max = null, int? offset = null)

Rules list

List of rules for the location for the given token principal

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class ListRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RulesApi(config);

            var locationId = locationId_example;  // string | The ID of the location to list the rules for.
            var max = 56;  // int? | The max number of rules to fetch (optional) 
            var offset = 56;  // int? | The start index of rules to fetch (optional) 

            try
            {
                // Rules list
                PagedRules result = apiInstance.ListRules(locationId, max, offset);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.ListRules: " + e.Message );
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
 **locationId** | **string**| The ID of the location to list the rules for. | 
 **max** | **int?**| The max number of rules to fetch | [optional] 
 **offset** | **int?**| The start index of rules to fetch | [optional] 

### Return type

[**PagedRules**](PagedRules.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A paginated list of rules. |  -  |
| **400** | Bad request |  -  |
| **401** | Not authenticated |  -  |
| **403** | Not authorized or not found |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updaterule"></a>
# **UpdateRule**
> Rule UpdateRule (string ruleId, string locationId, RuleRequest request)

Update a rule

Update a rule

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class UpdateRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new RulesApi(config);

            var ruleId = ruleId_example;  // string | The rule ID
            var locationId = locationId_example;  // string | The ID of the location in which to update the rule in.
            var request = new RuleRequest(); // RuleRequest | The rule to be updated.

            try
            {
                // Update a rule
                Rule result = apiInstance.UpdateRule(ruleId, locationId, request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.UpdateRule: " + e.Message );
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
 **ruleId** | **string**| The rule ID | 
 **locationId** | **string**| The ID of the location in which to update the rule in. | 
 **request** | [**RuleRequest**](RuleRequest.md)| The rule to be updated. | 

### Return type

[**Rule**](Rule.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The rule |  -  |
| **401** | Not authenticated |  -  |
| **403** | Not authorized or not found |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

