## MyMathSheets
項目的初衷是給自己在運用C#方面做一些嘗試以及在基本的框架設計上提供一個項目入口。<br/>
項目主題的確定其實與我的女兒有很大的關係，因為她剛入學在數學學習方面遇到了一些困難，那麼我的第一想法是做一個能夠自動出題的工具，希望能夠幫助到她。<br/>

## 項目的想法
項目設計和需求<br/>
>1. 題型模塊化（各題型之間沒有依賴關係）
>2. 題型能夠做到參數化配置（以便體現不同的難度適應學習習作的要求）
>3. 以HTML的形式展現（以適應更多的平台進行展示）
>4. 能夠對錯題提供修改的機會（有明顯的標誌顯示“對”與“錯”以免遺漏）
>5. 界面親切能夠更換主題（以體現新鮮感還有保護視力的重要性）
>6. 能夠記錄前一次的做題情況（包括做題所使用的時間，這很重要）
>7. 如果取得好成績那麼需要提供一些獎勵（包括展示得分、發放獎牌、會話鼓勵、簡單遊戲等等）
>8. 能夠有一位頁面虛擬人物（提示和幫助她完成某些題型）

![MyMathSheets](https://github.com/TonyZhangshi81/MyMathSheets/blob/master/Read/help01.jpg)

## 項目的實現
使用MEF框架做到各題型模塊之間相對獨立且鬆散耦合，ComposerFactory做到以插件方式動態加載各題型，期間ComputationalStrategy中各題型通過指定的參數配置作成策略，再由TheFormulaShows中相應的題型模塊提供HTML化後，由主框架HtmlSupportFactory完成HTML注入，最後由MainProcess來完成HTML文件的動態做成（與客戶設置畫面的分離）。至此，程序交由客戶端瀏覽器打開該HTML文件（小朋友可以做題了）
目前為止，整個解決方法都是圍繞著四則運算為主策略而進行的題型展開（後續增加了時間和貨幣相關的題型）。
從目錄結構上可以分為這樣幾個模塊：
>1. CommonLib 項目主框架模塊（主要完成對題型模塊管理、依賴注入、HTML注入等功能的實現和調度）
>2. ComputationalStrategy 題型策略類模塊（各題型以項目的形式存在，此處以解決方案統一管理這些題型）
>3. TestConsoleApp 以控制台的形式測試個題型策略是否正確（在得到頁面顯示模塊支持之間，題型參數的正確性是最重要的）
>4. TheFormulaShows 頁面顯示模塊（對題型策略類得到的各題型參數進行HTML化）
>5. MathSheetsSettingApp 提供窗口方式的題型選擇界面（以插件方式動態顯示各題型）
>6. Tools 題型參數化輔助配置工具（所有參數以json形式進行配置和管理）
上述模塊之間（除主框架模塊）彼此沒有引用關係，所以一般新題型設計完成後只需針對新題型完成策略類、頁面顯示類、題型測試類既可以上線（Lib文件夾統一管理各Assembly資源）。。。。當然，目前小組中就我一人：|

![MyMathSheets](https://github.com/TonyZhangshi81/MyMathSheets/blob/master/Read/help02.jpg)

## 目前已經完成的題型
> 1. Arithmetic 四則運算 AC
> 2. EqualityComparison 運算比大小 EC
> 3. ComputingConnection 等式接龍 CC
> 4. MathWordProblems 算式應用題 MP
> 5. FruitsLinkage 水果連連看 FL
> 6. FindNearestNumber 找到最近的數字 FN
> 7. CombinatorialEquation 算式組合 CE
> 8. ScoreGoal 射門得分 SG
> 9. HowMuchMore 比多少 HMM
>10. FindTheLaw 找規律 FTL
>11. NumericSorting 數字排序 NS
>12. LearnCurrency 認識貨幣 LC
>13. EqualityLinkage 算式連一連 EL
>14. SchoolClock 時鐘學習板 SC
>15. CurrencyOperation 貨幣運算 CO
>16. CurrencyLinkage 認識價格 CL
>17. TimeCalculation 時間運算 TC
>18. LearnLengthUnit 認識長度單位 LLU
>19. GapFillingProblems 基礎填空 GFP
>20. MathUpright 豎式計算 MU

![MyMathSheets](https://github.com/TonyZhangshi81/MyMathSheets/blob/master/Read/help03.jpg)
![MyMathSheets](https://github.com/TonyZhangshi81/MyMathSheets/blob/master/Read/help04.jpg)

## 項目的遠景
>1. 提供service API接口以提供出題配置（主程序web服務器部署）
目前只是一個cs結構的實現，因為設計之初考慮過頁面與策略分離，所以這個service接口完成是比較容易的
（當然對於並發的考慮還需要測試和主框架的調整）從項目的部署角度來說，由server來作成和管理各用戶的題型HTML文件並交由各client完成答題等一系列操作是可行的
>2. 用戶配置前端的完善
主要的考慮是想嘗試通過微信平台展示，如此HTML5、css3的標準化是首先需要攻克的，另外因為目前HTML文件的體量比較大（腳本、圖片）都需要精簡和優化（對TheFormulaShows可能是毀滅性的），這方面技術棧相當缺乏
>3. 對於虛擬人物的技術嘗試和揣摩（主要考慮交互性，目前加強對python的學習）
>4. 基於雲服務的學習和實踐也是迫在眉睫

## 謝謝女兒給我提供了一個好的機會，作為最終的用戶來說，項目成功與否主要來自於她對該項目的評價（是否喜歡數學）
#### 當然，這是有難度的，但首要的也是必須的要素是親切的、可交互的、多渠道的、高效的
#### 任重而道遠，我將繼續學習和嘗試！

