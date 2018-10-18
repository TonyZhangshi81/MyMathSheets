namespace MyMathSheets.CommonLib.Util
{
	/// <summary>
	/// 
	/// </summary>
	public class Consts
	{
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
		/// 計算式題型策略模塊
		/// </summary>
		ComputationalStrategy,
		/// <summary>
		/// HTML前台展示構築模塊
		/// </summary>
		TheFormulaShows
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
	/// 比多少（選擇項目：left為多的一方  Right為少的一方）
	/// </summary>
	/// <remarks>只適用於減法運算符</remarks>
	public enum MuchMoreSideType : int
	{
		/// <summary>
		/// 多的一方
		/// </summary>
		Left = 0,
		/// <summary>
		/// 少的一方
		/// </summary>
		Right
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
			/// 四則運算瀏覽
			/// </summary>
			Arithmetic,
			/// <summary>
			/// 運算比大小瀏覽
			/// </summary>
			EqualityComparison,
			/// <summary>
			/// 等式接龍瀏覽
			/// </summary>
			ComputingConnection,
			/// <summary>
			/// 算式應用題瀏覽
			/// </summary>
			MathWordProblems,
			/// <summary>
			/// 水果連連看瀏覽
			/// </summary>
			FruitsLinkage,
			/// <summary>
			/// 找到最近的數字瀏覽
			/// </summary>
			FindNearestNumber,
			/// <summary>
			/// 算式組合
			/// </summary>
			CombinatorialEquation,
			/// <summary>
			/// 射門得分
			/// </summary>
			ScoreGoal,
			/// <summary>
			/// 比多少
			/// </summary>
			HowMuchMore,
			/// <summary>
			/// 答題結束瀏覽
			/// </summary>
			Ready
		}
	}
}
