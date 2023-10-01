﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scryber.Components;
using Scryber.Html.Components;
using Scryber.Styles;
using Scryber.Drawing;
using Scryber.Styles.Parsing;

using Scryber.PDF.Layout;
using Scryber.PDF;
using System.Xml.Schema;
using Scryber.Svg.Components;
using System.IO;

namespace Scryber.Core.UnitTests.Html
{
    [TestClass()]
    public class SVGParsing_Tests
    {

        #region public TestContext TestContext

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion


        [TestMethod]
        public void SVGSimple()
        {
            var path = System.Environment.CurrentDirectory;
            path = System.IO.Path.Combine(path, "../../../Content/SVG/SVGSimple.html");

            using (var doc = Document.ParseDocument(path))
            {
                using (var stream = DocStreams.GetOutputStream("SVGSimple.pdf"))
                {
                    doc.RenderOptions.Compression = OutputCompressionType.None;
                    doc.SaveAsPDF(stream);

                    var section = doc.Pages[0] as Section;
                    Assert.IsNotNull(section);
                    Assert.AreEqual(3, section.Contents.Count);
                    Assert.IsInstanceOfType(section.Contents[0], typeof(HTMLParagraph));
                    Assert.IsInstanceOfType(section.Contents[2], typeof(HTMLParagraph));
 
                    var canvas = section.Contents[1] as Canvas;
                    Assert.IsNotNull(canvas);
                    Assert.AreEqual(300, canvas.Style.Size.Width.PointsValue, "The width of the canvas was not set");
                    Assert.AreEqual(200, canvas.Style.Size.Height.PointsValue, "The height of the canvas was not set");
                    Assert.AreEqual("canvas", canvas.StyleClass, "The style class of the canvas was not set");

                    Assert.AreEqual(1, canvas.Contents.Count);
                    var rect = canvas.Contents[0] as Svg.Components.SVGRect;
                    Assert.IsNotNull(rect, "The inner rectangle was not found");
                    Assert.AreEqual("box", rect.StyleClass, "The rect style class was not correct");
                    Assert.AreEqual(100, rect.Style.Position.X.PointsValue, "The X position of the rect was not correct");
                    Assert.AreEqual(10, rect.Style.Position.Y.PointsValue, "The Y position of the rect was not correct");
                    Assert.AreEqual(60, rect.Style.Size.Width.PointsValue, "The width of the rect was not correct");
                    Assert.AreEqual(70, rect.Style.Size.Height.PointsValue, "The height of the rect was not correct");
                    Assert.AreEqual(StandardColors.Green, rect.Style.Stroke.Color, "The stroke color of the rect was not set");
                    Assert.AreEqual(2, rect.Style.Stroke.Width.PointsValue, "The width of the stroke was not correct");
                    Assert.AreEqual(StandardColors.Yellow, rect.Style.Fill.Color, "The fill color of the rect was not correct");
                    Assert.AreEqual(20, rect.CornerRadiusX, "The x corner radius was not correct");
                    Assert.AreEqual(10, rect.CornerRadiusY, "The Y corner radius was not correct");
                }
            }
        }


        [TestMethod]
        public void SVGComponents()
        {
            var path = System.Environment.CurrentDirectory;
            path = System.IO.Path.Combine(path, "../../../Content/SVG/SVGComponents.html");

            using (var doc = Document.ParseDocument(path))
            {
                doc.RenderOptions.Compression = OutputCompressionType.None;

                using (var stream = DocStreams.GetOutputStream("SVGComponents.pdf"))
                {
                    doc.SaveAsPDF(stream);
                    
                    var section = doc.Pages[0] as Section;
                    Assert.IsNotNull(section);
                    var clock = section.Contents[1] as Canvas;
                    
                    Assert.IsNotNull(clock);
                    Assert.AreEqual("ClockIcon", clock.ID);
                    Assert.AreEqual(1, clock.Contents.Count);
                    Assert.AreEqual(20, clock.Width);
                    Assert.AreEqual(20, clock.Height);

                    var clockPath = clock.Contents[0] as SVGPath;
                    Assert.IsNotNull(clockPath);
                    Assert.AreEqual(StandardColors.Blue, clockPath.FillColor);
                }
            }
        }

        [TestMethod]
        public void SVGTransform()
        {
            var path = System.Environment.CurrentDirectory;
            path = System.IO.Path.Combine(path, "../../../Content/SVG/SVGTransform.html");

            using (var doc = Document.ParseDocument(path))
            {
                Div div;
                if(doc.TryFindAComponentById("mydiv", out div))
                {
                    var angle = 90.0;
                    angle = (Math.PI / 180.0) * angle;
                    div.Style.SetValue(StyleKeys.TransformOperationKey, new TransformOperation(TransformType.Rotate, (float)angle, TransformOperation.NotSetValue()));
                }

                using (var stream = DocStreams.GetOutputStream("Transform.pdf"))
                {
                    doc.SaveAsPDF(stream);
                }
            }
        }

        [TestMethod]
        public void SVGInsert()
        {
            var doc = new Document();
            var pg1 = new Page();
            doc.Pages.Add(pg1);

            var svgString = @"
            <svg xmlns=""http://www.w3.org/2000/svg"" style=""border:solid 1px black"" >
                <rect x=""0pt"" y=""0pt"" width=""100pt"" height=""80pt"" fill=""lime"" ></rect>
                <g id=""eye"" stroke=""black"" stroke-width=""2pt"" >
                    <ellipse cx=""50pt"" cy=""40pt"" rx=""40pt"" ry=""20pt"" fill=""white""></ellipse>
                    <circle cx=""50pt"" cy=""40pt"" r=""20pt"" fill=""#66F""></circle>
                    <circle cx=""50pt"" cy=""40pt"" r=""10pt"" fill=""black""></circle>
                    <line x1=""10"" x2=""90"" y1=""40"" y2=""40"" />
                    <line x1=""50"" x2=""50"" y1=""20"" y2=""60"" />
                </g>
            </svg>";



            //1. Explicit parsing of the content and adding to the page.
            //Allows handling of any parsing errors independently of the document flow.
            pg1.Contents.Add("The parsed component will be explicitly added after this.");
            var icomp = Document.Parse(new StringReader(svgString), ParseSourceType.DynamicContent);
            pg1.Contents.Add(icomp as Component);
            pg1.Contents.Add("This is after the parsed component");

            //2. Appending the data content to the page
            //Simply setting the DataContent to a string and the action as append.
            var pg2 = new Page();
            doc.Pages.Add(pg2);

            pg2.Contents.Add("The SVG content will be appended to the page.");
            pg2.DataContent = svgString;
            pg2.DataContentAction = DataContentAction.Append;
            pg2.Contents.Add(" Will still appear before the data content.");

            
            var pg3 = new Page();
            doc.Pages.Add(pg3);

            //3. Adding the content to an inner div (with some margins)
            //Gives flexibility in positioning and document flow.
            var div = new Div();
            div.DataContent = svgString;

            div.Margins = new Thickness(10);
            pg3.Contents.Add("Before the div with the inner svg");
            pg3.Contents.Add(div);
            pg3.Contents.Add("After the div with the inner svg");

            PDF.Layout.PDFLayoutDocument layout = null;
            //Output the document (including databinding the data content)
            using (var stream = DocStreams.GetOutputStream("SVGInserted.pdf"))
            {
                doc.LayoutComplete += (sender, args) =>
                {
                    layout = args.Context.GetLayout<PDF.Layout.PDFLayoutDocument>();
                };
                doc.SaveAsPDF(stream);
            }


            if (null == layout)
                throw new InvalidOperationException("Layout was not set");

            Assert.AreEqual(3, pg1.Contents.Count);
            var svg = pg1.Contents[1] as SVGCanvas;
            Assert.IsNotNull(svg);

            Assert.AreEqual(3, pg2.Contents.Count);
            svg = pg2.Contents[2] as SVGCanvas;
            Assert.IsNotNull(svg);

            Assert.AreEqual(3, pg3.Contents.Count);
            div = pg3.Contents[1] as Div;
            Assert.IsNotNull(div);
            Assert.AreEqual(1, div.Contents.Count);
            svg = div.Contents[0] as SVGCanvas;
            Assert.IsNotNull(svg);



            

        }

    }
}
