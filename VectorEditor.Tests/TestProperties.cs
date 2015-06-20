using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VectorEditor.Control;

namespace VectorEditor.Tests
{
    [TestClass]
    public class TestProperties
    {
        [TestMethod]
        public void TestRectanglePrimitiveProperties()
        {
            RectanglePrimitive primitive = new RectanglePrimitive(Guid.NewGuid(), 1, 1, 100, 100, 2, Colors.Blue);
            Assert.IsNotNull(primitive);

            RectanglePrimitiveProperties properties = primitive.CreateSerializedObject() as RectanglePrimitiveProperties;
            Assert.IsNotNull(properties);
            Assert.AreEqual(primitive.Id, properties.Id);
            Assert.AreEqual(primitive.LineWidth, properties.LineWidth);
            Assert.AreEqual(primitive.LineColor, properties.LineColor);
            Assert.AreEqual(primitive.X1, properties.X1);
            Assert.AreEqual(primitive.Y1, properties.Y1);
            Assert.AreEqual(primitive.X2, properties.X2);
            Assert.AreEqual(primitive.Y2, properties.Y2);

            RectanglePrimitive newPrimitive = properties.CreatePrimitive() as RectanglePrimitive;
            Assert.IsNotNull(newPrimitive);
            Assert.AreEqual(primitive.Id, newPrimitive.Id);
            Assert.AreEqual(primitive.LineWidth, newPrimitive.LineWidth);
            Assert.AreEqual(primitive.LineColor, newPrimitive.LineColor);
            Assert.AreEqual(primitive.X1, newPrimitive.X1);
            Assert.AreEqual(primitive.Y1, newPrimitive.Y1);
            Assert.AreEqual(primitive.X2, newPrimitive.X2);
            Assert.AreEqual(primitive.Y2, newPrimitive.Y2);
        }

        [TestMethod]
        public void TestLinePrimitiveProperties()
        {
            Point[] points = { new Point(1, 1), new Point(100, 1), new Point(100, 100) };
            PolyLinePrimitive primitive = new PolyLinePrimitive(Guid.NewGuid(), points, 3, Colors.Black);
            Assert.IsNotNull(primitive);
            
            LinePrimitiveProperties properties = primitive.CreateSerializedObject() as LinePrimitiveProperties;
            Assert.IsNotNull(properties);
            Assert.AreEqual(primitive.Id, properties.Id);
            Assert.AreEqual(primitive.LineWidth, properties.LineWidth);
            Assert.AreEqual(primitive.LineColor, properties.LineColor);
            Assert.AreEqual(primitive.Points.Count, properties.Points.Length);
            Assert.AreEqual(primitive.Points[0], properties.Points[0]);
            Assert.AreEqual(primitive.Points[1], properties.Points[1]);
            Assert.AreEqual(primitive.Points[2], properties.Points[2]);

            PolyLinePrimitive newPrimitive = properties.CreatePrimitive() as PolyLinePrimitive;
            Assert.IsNotNull(newPrimitive);
            Assert.AreEqual(primitive.Id, newPrimitive.Id);
            Assert.AreEqual(primitive.LineWidth, newPrimitive.LineWidth);
            Assert.AreEqual(primitive.LineColor, newPrimitive.LineColor);
            Assert.AreEqual(primitive.Points.Count, newPrimitive.Points.Count);
            Assert.AreEqual(primitive.Points[0], newPrimitive.Points[0]);
            Assert.AreEqual(primitive.Points[1], newPrimitive.Points[1]);
            Assert.AreEqual(primitive.Points[2], newPrimitive.Points[2]);
        }

    }
}