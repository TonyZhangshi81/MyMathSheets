namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public class Consts
	{
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
	/// 四则运算类型（标准：指定一種運算符、随机出题：隨機抽取一種運算符）
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
