- [1. 簡介](#1-簡介)
- [2. 設計](#2-設計)
- [3. 使用](#3-使用)

## 1. 簡介
健康檢查通常是一個HTTP端點，它提供有關系統狀態的信息。它可以像提供關於單個站點的啟動或關閉的信息一樣簡單，也可以更複雜，並提供關於系統及其依賴項的狀態信息，例如數據庫、郵件服務器和站點連接的HTTP端點。
當構建微服務架構時，後者尤其有用，因為你需要觀察大量相互連接的服務以確定系統的整體健康程度。

## 2. 設計
首先是`IHealthCheckProvider`接口，它定義了一個方法來執行某些健康檢查（圖例中的`DiskSpaceHealthCheckProvider`、`A_HealthCheckProvider`、`B_HealthCheckProvider`、`C_HealthCheckProvider`等等各類檢查的實現），通過各自實現類中的`GetHealthCheckAsync`方法將結果返回（使用統一的類型`HealthCheckItemResult`）
為了將組合的健康狀態返回給客戶端，使用並創建一個結果容器`DTO（HealthCheckResult）`結果類集合。容器保存單個檢查結果的列表以及一些總體的、機器範圍的信息。
下圖顯示了解決方案的整體類層次結構：

![MyMathSheets.HealthChecks](https://github.com/TonyZhangshi81/MyMathSheets/raw/master/Read/classview.png)

* 在 2.0.1 版本中擴展了`MemoryHealthCheckPovider`(內存狀態檢查)，在上圖中並未標識

## 3. 使用
通过在当前应用域中找到实现`IHealthCheckProvider`接口的所有类（不包含抽象类），通过各自实现的`SortOrder`依次执行并获得它们的健康状态，将它们的结果添加到`HealthCheckResult`容器中，最后将结果作为`JSON`返回给客户端。
調用方`Webapi`也同樣可以使用依賴注入引入框架中`IHealthController`的具體實現，一下代碼為例：

```cs
// 依賴注入
[Import("HealthCheck", typeof(IHealthController))]
public WebHealthChecks.Base.HealthController HealthCheck { get; set; }

// 檢查實施
var result = await HealthCheck.GetHealthInfoAsync();
HttpResponseMessage responseMessage = new HttpResponseMessage
{
    // json格式構築應答
    Content = new StringContent(result.GetJsonByObject(), encoding: Encoding.GetEncoding("UTF-8"), mediaType: "applicaton/json"),
    StatusCode = HttpStatusCode.OK
};
```

示例中配置`webapi`的`api/Health/Check`端點將以`JSON`格式返回檢查結果。
`hasFailures`屬性指示系統是否處於穩定狀態（或者是否存在任何問題）。
`healthChecks`集合下的項提供有關已檢查項的附加信息以及它們被視為健康或者不健康的原因。

