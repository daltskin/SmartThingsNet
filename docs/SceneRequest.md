# Org.OpenAPITools.Model.SceneRequest
JSON body for creating or updating a Scene
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**SceneName** | **string** | The user-defined name of the Scene | 
**SceneIcon** | **string** | The name of the icon | [optional] 
**SceneColor** | **string** | The color of the icon | [optional] 
**Devices** | [**List&lt;SceneDeviceRequest&gt;**](SceneDeviceRequest.md) | Non-sequential list of device actions | 
**Sequences** | **List&lt;List&gt;** | List of parallel action sequences | [optional] 
**Mode** | [**SceneModeRequest**](SceneModeRequest.md) |  | [optional] 
**SecurityMode** | [**SceneSecurityModeRequest**](SceneSecurityModeRequest.md) |  | [optional] 
**Devicegroups** | [**List&lt;SceneDeviceGroupRequest&gt;**](SceneDeviceGroupRequest.md) | List of device group actions | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

