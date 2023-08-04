using MyMathSheets.CommonLib.Main.Calculate;
using MyMathSheets.CommonLib.Main.Item;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Main.Policy.Attributes;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.ComputationalStrategy.ScoreGoal.Main.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace MyMathSheets.ComputationalStrategy.ScoreGoal.Main.Strategy
{
    /// <summary>
    /// 射門得分題型構築
    /// </summary>
    [Topic("ScoreGoal")]
    public class ScoreGoal : TopicBase<ScoreGoalParameter>
    {
        // 球類算式實例
        private readonly Dictionary<Formula, int> _ballsFormulas;

        // 球門算式實例
        private readonly IList<Formula> _goalsFormulas;

        /// <summary>
        /// 構造函數
        /// </summary>
        [ImportingConstructor]
        public ScoreGoal()
        {
            _ballsFormulas = new Dictionary<Formula, int>();
            _goalsFormulas = new List<Formula>();
        }

        /// <summary>
        /// 策略出題之前的準備工作
        /// </summary>
        /// <param name="p">題型參數</param>
        protected override void PreMarkFormulaList(ScoreGoalParameter p)
        {
            _ballsFormulas?.Clear();
            _goalsFormulas?.Clear();
        }

        /// <summary>
        /// 左側計算式集合作成并返回當前新作成的計算式
        /// </summary>
        /// <param name="maximumLimit">計算結果最大值</param>
        /// <param name="signFunc">運算符取得用的表達式</param>
        /// <returns>新作成的計算式</returns>
        private Formula MakeLeftFormulas(int maximumLimit, Func<SignOfOperation> signFunc)
        {
            IArithmetic strategy = CalculateManager(signFunc());

            // 計算式作成
            Formula formula = strategy.CreateFormula(new ArithmeticParameter()
            {
                MaximumLimit = maximumLimit,
                QuestionType = QuestionType.Default,
                MinimumLimit = 0
            });

            return formula;
        }

        /// <summary>
        /// 右側計算式集合作成并返回當前新作成的計算式
        /// </summary>
        /// <param name="maximumLimit">計算結果最大值</param>
        /// <param name="leftFormulaAnswer">左側新作成計算式的結果值</param>
        /// <param name="signFunc">運算符取得用的表達式</param>
        /// <returns>新作成的計算式</returns>
        private Formula MakeRightFormulas(int maximumLimit, int leftFormulaAnswer, Func<SignOfOperation> signFunc)
        {
            IArithmetic strategy = CalculateManager(signFunc());

            // 計算式作成（依據左邊算式的答案推算右邊的算式）
            Formula formula = strategy.CreateFormulaWithAnswer(new ArithmeticParameter()
            {
                MaximumLimit = maximumLimit,
                QuestionType = QuestionType.Default,
                MinimumLimit = 0
            }, leftFormulaAnswer);

            return formula;
        }

        /// <summary>
        /// 計算式作成處理
        /// </summary>
        /// <param name="p">題型參數</param>
        /// <param name="signFunc">運算符取得用的表達式</param>
        private void MarkFormulaList(ScoreGoalParameter p, Func<SignOfOperation> signFunc)
        {
            // 按照指定數量作成相應的球門數學計算式(兩個球門)
            for (int i = 0; i < 2; i++)
            {
                // 計算式作成
                Formula goal = MakeLeftFormulas(p.MaximumLimit, signFunc);
                // 判定是否需要反推并重新作成計算式
                if (CheckIsNeedInverseMethodForGoals(goal, _goalsFormulas))
                {
                    i--;
                    continue;
                }
                _goalsFormulas.Add(goal);
            }
            // 按照指定數量作成相應的數學計算式(足球的個數最多10個)
            for (int i = 0; i < p.NumberOfQuestions; i++)
            {
                int seat = 0;
                // 選取球門
                int answer = GetGoal(_goalsFormulas, ref seat);
                // 足球计算式
                Formula formula = MakeRightFormulas(p.MaximumLimit, answer, signFunc);
                // 判定是否需要反推并重新作成計算式
                if (CheckIsNeedInverseMethodForBalls(formula, _ballsFormulas))
                {
                    i--;
                    continue;
                }
                // 足球計算式
                _ballsFormulas.Add(formula, seat);
            }
        }

        /// <summary>
        /// 計算式作成處理
        /// </summary>
        /// <param name="p">參數</param>
        public override void MarkFormulaList(ScoreGoalParameter p)
        {
            _ballsFormulas.Clear();
            _goalsFormulas.Clear();

            // (足球的個數最多10個)
            p.NumberOfQuestions = (p.NumberOfQuestions > 10) ? 10 : p.NumberOfQuestions;
            // 標準題型（指定單個運算符）
            if (p.FourOperationsType == FourOperationsType.Standard)
            {
                // 計算式作成（指定單個運算符實例）
                MarkFormulaList(p, () => { return p.Signs[0]; });

                p.Formulas.GoalsFormulas = _goalsFormulas;
                p.Formulas.BallsFormulas = _ballsFormulas;
            }
            else
            {
                // 計算式作成（加減乘除運算符實例隨機抽取）
                MarkFormulaList(p, () => { return p.Signs[CommonUtil.GetRandomNumber(0, p.Signs.Count - 1)]; });

                p.Formulas.GoalsFormulas = _goalsFormulas;
                p.Formulas.BallsFormulas = _ballsFormulas;
            }
        }

        /// <summary>
        /// 選取球門
        /// </summary>
        /// <param name="goalsFormulas">球門算式集合</param>
        /// <param name="seat">球門號(1號球門或者0號球門)</param>
        /// <returns>被選擇的計算結果</returns>
        private int GetGoal(IList<Formula> goalsFormulas, ref int seat)
        {
            seat = CommonUtil.GetRandomNumber(0, 1);
            // 在兩個球門之間隨機選擇(選擇的球門號)
            return goalsFormulas[seat].Answer;
        }

        /// <summary>
        /// 判定是否需要反推并重新作成計算式
        /// </summary>
        /// <remarks>
        /// 情況1：計算式中三個數值都為零
        /// 情況3：算式一致的計算式
        /// </remarks>
        /// <param name="currentFormula">當前算式</param>
        /// <param name="ballsFormulas">已有的算式集合</param>
        /// <returns>需要反推：true  正常情況: false</returns>
        private bool CheckIsNeedInverseMethodForBalls(Formula currentFormula, Dictionary<Formula, int> ballsFormulas)
        {
            // 等式左邊參數都是零的情況
            if (currentFormula.RightParameter == 0 || currentFormula.LeftParameter == 0)
            {
                return true;
            }
            // 算式存在一致
            if (ballsFormulas.Count > 1 && ballsFormulas.ToList().Any(d => d.Key.RightParameter == currentFormula.RightParameter
                                            && d.Key.LeftParameter == currentFormula.LeftParameter
                                            && d.Key.Answer == currentFormula.Answer
                                            && d.Key.Sign == currentFormula.Sign))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判定是否需要反推并重新作成計算式
        /// </summary>
        /// <remarks>
        /// 情況1：計算式中三個數值都為零
        /// 情況3：答案一致的兩個球門不允許
        /// </remarks>
        /// <param name="currentFormula">球門A</param>
        /// <param name="goalsFormulas">球門B</param>
        /// <returns>需要反推：true  正常情況: false</returns>
        private bool CheckIsNeedInverseMethodForGoals(Formula currentFormula, IList<Formula> goalsFormulas)
        {
            // 等式左邊參數都是零的情況
            if (currentFormula.RightParameter == 0 || currentFormula.LeftParameter == 0 || currentFormula.Answer == 0)
            {
                return true;
            }

            // 答案一致的兩個球門不允許
            if (goalsFormulas.Count == 1 && currentFormula.Answer == goalsFormulas[0].Answer)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 資源釋放
        /// </summary>
        protected override void DisposeManaged()
        {
            if (_ballsFormulas != null)
            {
                _ballsFormulas.Clear();
            }
            if (_goalsFormulas != null)
            {
                _goalsFormulas.Clear();
            }
        }
    }
}