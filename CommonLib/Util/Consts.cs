namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public class Consts
	{
	}

	/// <summary>
	/// 貨幣運算類型
	/// </summary>
	public enum CurrencyOperationUnitType : int
	{
		/// <summary>
		/// 元角分單位
		/// </summary>
		YJF = 0,
		/// <summary>
		/// 元角單位
		/// </summary>
		YJ,
		/// <summary>
		/// 元分單位
		/// </summary>
		YF,
		/// <summary>
		/// 元單位
		/// </summary>
		Yuan,
		/// <summary>
		/// 角單位
		/// </summary>
		Jiao,
		/// <summary>
		/// 角分單位
		/// </summary>
		JF,
		/// <summary>
		/// 分單位
		/// </summary>
		Fen
	}

	/// <summary>
	/// 貨幣轉換題型種類
	/// </summary>
	/// <remarks>轉換的前提不出現小數點</remarks>
	public enum CurrencyTransform : int
	{
		/// <summary>
		/// 元到角(eg:1元=>10角)
		/// </summary>
		Y2J = 0,
		/// <summary>
		/// 元到分(eg:1元=>100分)
		/// </summary>
		Y2F,
		/// <summary>
		/// 角轉元(eg:20角=>2元)
		/// </summary>
		J2Y,
		/// <summary>
		/// 角轉分(eg:2角=>20分)
		/// </summary>
		J2F,
		/// <summary>
		/// 角轉元分(eg:34角=>3元4分)
		/// </summary>
		J2YF,
		/// <summary>
		/// 分轉元(eg:200分=>2元)
		/// </summary>
		F2Y,
		/// <summary>
		/// 分轉角(eg:20分=>2角)
		/// </summary>
		F2J,
		/// <summary>
		/// 分轉元角(eg:230分=>2元3角)
		/// </summary>
		F2YJ,
		/// <summary>
		/// 角轉元(有剩餘 eg:23角=>2元3角)
		/// </summary>
		J2YExt,
		/// <summary>
		/// 分轉元角(有剩餘 eg:234分=>2元3角4分)
		/// </summary>
		F2YJExt,
	}

	/// <summary>
	/// 小時分割（0、1/4、1/2、3/4小時）
	/// </summary>
	public enum HourDivision : int
	{
		/// <summary>
		/// 小時整點(0分)
		/// </summary>
		IntegralPoint = 0,
		/// <summary>
		/// 四分之一小時(15分)
		/// </summary>
		Quarter = 1,
		/// <summary>
		/// 二分之一小時(30分)
		/// </summary>
		Half = 2,
		/// <summary>
		/// 四分之三小時(45分)
		/// </summary>
		ThreeQuarters = 3
	}

	/// <summary>
	/// 計時制（AM/PM）
	/// </summary>
	public enum TimeSystem : int
	{
		/// <summary>
		/// 上午[00:01~12:00]
		/// </summary>
		AM = 0,
		/// <summary>
		/// 下午[12:01~24:00]
		/// </summary>
		PM = 1
	}

	/// <summary>
	/// 時間段類型（24小時制）
	/// </summary>
	public enum TimeIntervalType : int
	{
		/// <summary>
		/// 午夜[0:XX]
		/// </summary>
		Midnight = 0,
		/// <summary>
		/// 凌晨[1:XX~5:XX]
		/// </summary>
		WeeHours = 1,
		/// <summary>
		/// 上午[6:XX~11:XX]
		/// </summary>
		Forenoon = 2,
		/// <summary>
		/// 中午[12:XX]
		/// </summary>
		Nooning = 3,
		/// <summary>
		/// 下午[13:XX~18:XX]
		/// </summary>
		Afternoon = 4,
		/// <summary>
		/// 晚上[19:XX~21:XX]
		/// </summary>
		Night = 5,
		/// <summary>
		/// 深夜[22:XX~23:XX]
		/// </summary>
		LateNight = 6
	}

	/// <summary>
	/// 連線題左右排列類型(橫向連線和縱向連線)
	/// </summary>
	public enum DivQueueType : int
	{
		/// <summary>
		/// 橫向連線
		/// </summary>
		Crosswise = 0,
		/// <summary>
		/// 縱向連線
		/// </summary>
		Lengthways = 1
	}

	/// <summary>
	/// Message消息級別
	/// </summary>
	public enum MessageLevel
	{
		/// <summary>
		/// 調試信息
		/// </summary>
		Debug,
		/// <summary>
		/// 業務異常信息
		/// </summary>
		Error,
		/// <summary>
		/// 系統異常信息
		/// </summary>
		Fatal,
		/// <summary>
		/// 一般消息
		/// </summary>
		Info,
		/// <summary>
		/// 警告信息
		/// </summary>
		Warn
	}

	/// <summary>
	/// 系統模塊
	/// </summary>
	public enum SystemModel
	{
		/// <summary>
		/// 共通定義模塊（含基類）
		/// </summary>
		Common,
		/// <summary>
		/// 基本運算處理模塊（加減乘除）
		/// </summary>
		BasicOperations,
		/// <summary>
		/// 計算式題型策略模塊
		/// </summary>
		ComputationalStrategy,
		/// <summary>
		/// HTML前台展示構築模塊
		/// </summary>
		TheFormulaShows,
		/// <summary>
		/// 應用程序窗體
		/// </summary>
		MathSheetsSettingApp
	}

	/// <summary>
	/// 比多少題中條件顯示（左邊還是右邊）
	/// </summary>
	public enum LeftOrRight : int
	{
		/// <summary>
		/// 左邊
		/// </summary>
		Left = 0,
		/// <summary>
		/// 右邊
		/// </summary>
		Right
	}

	/// <summary>
	/// 比多少題中條件顯示（多的還是少的）
	/// </summary>
	public enum MoreOrLess : int
	{
		/// <summary>
		/// 多的
		/// </summary>
		More = 0,
		/// <summary>
		/// 少的
		/// </summary>
		Less,
	}

	/// <summary>
	/// 运算符（加、減、乘、除）
	/// </summary>
	public enum SignOfOperation : int
	{
		/// <summary>
		/// 加号
		/// </summary>
		Plus = 0,
		/// <summary>
		/// 减号
		/// </summary>
		Subtraction,
		/// <summary>
		/// 乘号
		/// </summary>
		Multiple,
		/// <summary>
		/// 除号
		/// </summary>
		Division
	}

	/// <summary>
	/// 填空随机项目
	/// </summary>
	public enum GapFilling : int
	{
		/// <summary>
		/// 运算符左边参数
		/// </summary>
		Left = 0,
		/// <summary>
		/// 运算符右边参数
		/// </summary>
		Right,
		/// <summary>
		/// 等式结果(符号个数 = 3)
		/// </summary>
		Answer,
		/// <summary>
		/// 
		/// </summary>
		Default
	}

	/// <summary>
	/// 题型（标准:結果項為填空、随机：等式中隨機選擇一個項目為填空）
	/// </summary>
	public enum QuestionType : int
	{
		/// <summary>
		/// 标准
		/// </summary>
		Standard = 1,
		/// <summary>
		/// 随机填空
		/// </summary>
		GapFilling,
		/// <summary>
		/// 默认值(未设定)
		/// </summary>
		Default
	}

	/// <summary>
	/// 四则运算类型（标准：指定一種運算符、随机出题：隨機抽取一種運算符、時鐘學習板是否指定分鐘）
	/// </summary>
	public enum FourOperationsType : int
	{
		/// <summary>
		/// 未指定
		/// </summary>
		Default = 0,
		/// <summary>
		/// 标准
		/// </summary>
		Standard,
		/// <summary>
		/// 随机出题（加减乘除）
		/// </summary>
		Random
	}

	/// <summary>
	/// 比多少題型的圖片名稱
	/// </summary>
	public enum HowMuchMoreType : int
	{
		/// <summary>
		/// 圓的
		/// </summary>
		Circle = 0,
		/// <summary>
		/// 菱形格
		/// </summary>
		Diamond,
		/// <summary>
		/// 魚形
		/// </summary>
		Fish,
		/// <summary>
		/// 笑臉
		/// </summary>
		HappyFace,
		/// <summary>
		/// 漢堡
		/// </summary>
		Humburger,
		/// <summary>
		/// 愛心
		/// </summary>
		Like,
		/// <summary>
		/// 方形
		/// </summary>
		Square
	}

	/// <summary>
	/// 比较运算符
	/// </summary>
	public enum SignOfCompare : int
	{
		/// <summary>
		/// 大于
		/// </summary>
		Greater = 0,
		/// <summary>
		/// 小于
		/// </summary>
		Less,
		/// <summary>
		/// 等于
		/// </summary>
		Equal
	}

	/// <summary>
	/// 球類
	/// </summary>
	public enum Balls : int
	{
		/// <summary>
		/// 棒球
		/// </summary>
		Ball = 0,
		/// <summary>
		/// 足球
		/// </summary>
		Basketball,
		/// <summary>
		/// 皮球
		/// </summary>
		BeachBall,
		/// <summary>
		/// 保齡球
		/// </summary>
		Bowling,
		/// <summary>
		/// 足球
		/// </summary>
		Football,
		/// <summary>
		/// 高爾夫球
		/// </summary>
		Golf,
		/// <summary>
		/// 橄欖球
		/// </summary>
		Rugby,
		/// <summary>
		/// 網球
		/// </summary>
		Tennis,
		/// <summary>
		/// 排球
		/// </summary>
		Volleyball
	}

	/// <summary>
	/// 商品
	/// </summary>
	public enum Shop : int
	{
		/// <summary>
		/// 手套
		/// </summary>
		Mittens = 0,
		/// <summary>
		/// 玩偶
		/// </summary>
		Christmas,
		/// <summary>
		/// 書包
		/// </summary>
		Schoolbag,
		/// <summary>
		/// 拖鞋
		/// </summary>
		Slipper,
		/// <summary>
		/// 帽子
		/// </summary>
		Hat,
		/// <summary>
		/// 體恤衫
		/// </summary>
		Shirt,
		/// <summary>
		/// 書
		/// </summary>
		Book,
		/// <summary>
		/// 尺
		/// </summary>
		Ruler,
		/// <summary>
		/// 鉛筆
		/// </summary>
		Pencil,
		/// <summary>
		/// 橡皮
		/// </summary>
		Rubber,
		/// <summary>
		/// 雨傘
		/// </summary>
		Umbrella,
		/// <summary>
		/// 魔方
		/// </summary>
		RubiksCube,
		/// <summary>
		/// 貨幣
		/// </summary>
		Money
	}

	/// <summary>
	/// 水果
	/// </summary>
	public enum Fruits : int
	{
		/// <summary>
		/// 蘋果
		/// </summary>
		Apple = 0,
		/// <summary>
		/// 木瓜
		/// </summary>
		Apricot,
		/// <summary>
		/// 香蕉
		/// </summary>
		Banana,
		/// <summary>
		/// 櫻桃
		/// </summary>
		Cherry,
		/// <summary>
		/// 葡萄
		/// </summary>
		Grape,
		/// <summary>
		/// 哈密瓜
		/// </summary>
		Hamimelon,
		/// <summary>
		/// 桔子
		/// </summary>
		Orange,
		/// <summary>
		/// 桃子
		/// </summary>
		Peach,
		/// <summary>
		/// 梨
		/// </summary>
		Pear,
		/// <summary>
		/// 草莓
		/// </summary>
		Strawberry,
		/// <summary>
		/// 西瓜
		/// </summary>
		Watermelon
	}

	/// <summary>
	/// 找規律相關的枚舉類
	/// </summary>
	public enum FindTheLawLevel : int
	{
		/// <summary>
		/// 定值逐漸增大型
		/// </summary>
		Crescent,
		/// <summary>
		/// 逐漸減小型
		/// </summary>
		Diminishingly,
		/// <summary>
		/// 疊加型
		/// </summary>
		Superposition,
	}

	/// <summary>
	/// 題型類型
	/// </summary>
	public class LayoutSetting
	{
		/// <summary>
		/// 題型類型/瀏覽設定
		/// </summary>
		public enum Preview : int
		{
			/// <summary>
			/// 標題瀏覽
			/// </summary>
			Title = 0,
			/// <summary>
			/// 四則運算 AC
			/// </summary>
			Arithmetic,
			/// <summary>
			/// 運算比大小 EC
			/// </summary>
			EqualityComparison,
			/// <summary>
			/// 等式接龍 CC
			/// </summary>
			ComputingConnection,
			/// <summary>
			/// 算式應用題 MP
			/// </summary>
			MathWordProblems,
			/// <summary>
			/// 水果連連看 FL
			/// </summary>
			FruitsLinkage,
			/// <summary>
			/// 找到最近的數字 FN
			/// </summary>
			FindNearestNumber,
			/// <summary>
			/// 算式組合 CE
			/// </summary>
			CombinatorialEquation,
			/// <summary>
			/// 射門得分 SG
			/// </summary>
			ScoreGoal,
			/// <summary>
			/// 比多少 HMM
			/// </summary>
			HowMuchMore,
			/// <summary>
			/// 找規律 FTL
			/// </summary>
			FindTheLaw,
			/// <summary>
			/// 數字排序 NS
			/// </summary>
			NumericSorting,
			/// <summary>
			/// 認識貨幣
			/// </summary>
			LearnCurrency,
			/// <summary>
			/// 算式連一連
			/// </summary>
			EqualityLinkage,
			/// <summary>
			/// 時鐘學習板
			/// </summary>
			SchoolClock,
			/// <summary>
			/// 貨幣運算
			/// </summary>
			CurrencyOperation,
			/// <summary>
			/// 認識價格
			/// </summary>
			CurrencyLinkage,
			/// <summary>
			/// 答題結束瀏覽
			/// </summary>
			Ready,
			/// <summary>
			/// 未設定
			/// </summary>
			Null
		}
	}
}
