# SmartThingsNet.Model.Device
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DeviceId** | **string** | The identifier for the device instance. | [optional] 
**Name** | **string** | The name that the device integration (Device Handler or SmartApp) defines for the device. | [optional] 
**Label** | **string** | The name that a user chooses for the device. This defaults to the same value as name. | [optional] 
**DeviceManufacturerCode** | **string** | The device manufacturer code. | [optional] 
**LocationId** | **string** | The ID of the Location with which the device is associated. | [optional] 
**RoomId** | **string** | The ID of the Room with which the device is associated. If the device is not associated with any room, then this field will be null. | [optional] 
**DeviceTypeId** | **string** | Deprecated please look under \&quot;dth\&quot;. | [optional] 
**DeviceTypeName** | **string** | Deprecated please look under \&quot;dth\&quot;. | [optional] 
**DeviceNetworkType** | **string** | Deprecated please look under \&quot;dth\&quot;. | [optional] 
**Components** | [**List&lt;DeviceComponent&gt;**](DeviceComponent.md) | The IDs of all compenents on the device. | [optional] 
**ChildDevices** | [**List&lt;Device&gt;**](Device.md) | Device details for all child devices of the device. | [optional] 
**Profile** | [**DeviceProfileReference**](DeviceProfileReference.md) |  | [optional] 
**App** | [**AppDeviceDetails**](AppDeviceDetails.md) |  | [optional] 
**Dth** | [**DthDeviceDetails**](DthDeviceDetails.md) |  | [optional] 
**Ir** | [**IrDeviceDetails**](IrDeviceDetails.md) |  | [optional] 
**IrOcf** | [**IrDeviceDetails**](IrDeviceDetails.md) |  | [optional] 
**Viper** | [**ViperDeviceDetails**](ViperDeviceDetails.md) |  | [optional] 
**Type** | **DeviceIntegrationType** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

