using FunctionEvaluatorLibrary.Function;
using System;
using Xunit;

namespace FunctionEvaluatorLibrary.Tests
{
    public class FunctionEvaluatorTest
    {
        private double actualResult;
        private double roundedSolution;        
        private double tolerance = 0.0001;

        private double upperLimit;
        private double lowerLimit;

        private bool isAboveLowerLimmit;
        private bool isUnderUpperLimit;

        [Fact]
        public void SimpleFunctionEvaluatonWithPowerAndBasicOperationsTest()
        {
            SimpleFunction testFunction = new SimpleFunction("x^2+3x-23");
            Assert.Equal(-5, testFunction.Evaluate(3));
        }

        [Fact]
        public void SimpleFunctionEvaluatonWithSinOperationTest()
        {
            SimpleFunction testFunction = new SimpleFunction("x+sin(x)");
            actualResult = testFunction.Evaluate(2);

            roundedSolution = 2.90929;

            upperLimit = roundedSolution + tolerance;
            lowerLimit = roundedSolution - tolerance;

            isAboveLowerLimmit = actualResult > lowerLimit;
            isUnderUpperLimit = actualResult < upperLimit;

            Assert.True(isAboveLowerLimmit && isUnderUpperLimit);
        }

        [Fact]
        public void SimpleFunctionEvaluatonWithTanOperationTest()
        {
            SimpleFunction testFunction = new SimpleFunction("x*tan(2x)+2");
            actualResult = testFunction.Evaluate(3.2);

            roundedSolution = 2.37551;

            upperLimit = roundedSolution + tolerance;
            lowerLimit = roundedSolution - tolerance;

            isAboveLowerLimmit = actualResult > lowerLimit;
            isUnderUpperLimit = actualResult < upperLimit;

            Assert.True(isAboveLowerLimmit && isUnderUpperLimit);
        }

        [Fact]
        public void SimpleFunctionEvaluatonWithRootOperationTest()
        {
            SimpleFunction testFunction = new SimpleFunction("root(x+2,3)+2.1");
            actualResult = testFunction.Evaluate(5.21);

            roundedSolution = 4.03187;

            upperLimit = roundedSolution + tolerance;
            lowerLimit = roundedSolution - tolerance;

            isAboveLowerLimmit = actualResult > lowerLimit;
            isUnderUpperLimit = actualResult < upperLimit;

            Assert.True(isAboveLowerLimmit && isUnderUpperLimit);
        }
    }
}
