﻿/*  Copyright 2012 PerceiveIT Limited
 *  This file is part of the Scryber library.
 *
 *  You can redistribute Scryber and/or modify 
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 * 
 *  Scryber is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 * 
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with Scryber source code in the COPYING.txt file.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Scryber;
using Scryber.Drawing;

namespace Scryber.Styles
{
    [PDFParsableComponent("Page")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [PDFJSConvertor("scryber.studio.design.convertors.styleItem", JSParams = "\"page\"")]
    public class PDFPageStyle : PDFStyleItemBase
    {

        #region public PaperSize PaperSize {get;set;} + RemovePaperSize()

        [PDFAttribute("size")]
        [PDFJSConvertor("scryber.studio.design.convertors.papersize_css")]
        [PDFDesignable("Size", Category = "Paper", Priority = 1, Type = "Select")]
        public PaperSize PaperSize
        {
            get
            {
                PaperSize val;
                if (this.TryGetValue(PDFStyleKeys.PagePaperSizeKey, out val))
                    return val;
                else
                    return Const.DefaultPaperSize;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PagePaperSizeKey, value);
            }
        }

        public void RemovePaperSize()
        {
            this.RemoveValue(PDFStyleKeys.PagePaperSizeKey);
        }

        #endregion

        #region public PaperOrientation PaperOrientation {get;set} + RemovePaperOrientation()

        [PDFAttribute("orientation")]
        [PDFJSConvertor("scryber.studio.design.convertors.paperorientation_css")]
        [PDFDesignable("Orientation", Category = "Paper", Priority = 1, Type = "Select")]
        public PaperOrientation PaperOrientation
        {
            get
            {
                PaperOrientation val;
                if (this.TryGetValue(PDFStyleKeys.PageOrientationKey, out val))
                    return val;
                else
                    return Const.DefaultPaperOrientation;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageOrientationKey, value);
            }
        }

        public void RemovePaperOrientation()
        {
            this.RemoveValue(PDFStyleKeys.PageOrientationKey);
        }

        #endregion

        #region public PDFUnit Width {get;set;} + RemoveWidth()

        [PDFAttribute("width")]
        public PDFUnit Width
        {
            get
            {
                PDFUnit f;
                if (this.TryGetValue(PDFStyleKeys.PageWidthKey, out f))
                    return f;
                else
                    return PDFUnit.Empty;

            }
            set
            {
                this.SetValue(PDFStyleKeys.PageWidthKey, value);
            }
        }

        public void RemoveWidth()
        {
            this.RemoveValue(PDFStyleKeys.PageWidthKey);
        }

        #endregion

        #region public PDFUnit Height {get;set;} + RemoveHeight()

        [PDFAttribute("height")]
        public PDFUnit Height
        {
            get
            {
                PDFUnit f;
                if (this.TryGetValue(PDFStyleKeys.PageHeightKey, out f))
                    return f;
                else
                    return PDFUnit.Empty;

            }
            set
            {
                this.SetValue(PDFStyleKeys.PageHeightKey, value);
            }
        }

        public void RemoveHeight()
        {
            this.RemoveValue(PDFStyleKeys.PageHeightKey);
        }

        #endregion

        #region public PageNumberStyle NumberStyle {get;set;} + RemoveNumberStyle()

        [PDFAttribute("number-style")]
        [PDFDesignable("Numbering Style", Category = "Paper", Priority = 1, Type = "Select")]
        public PageNumberStyle NumberStyle
        {
            get
            {
                PageNumberStyle style;
                if (this.TryGetValue(PDFStyleKeys.PageNumberStyleKey, out style))
                    return style;
                else
                    return PageNumberStyle.Decimals;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberStyleKey, value);
            }
        }

        public void RemoveNumberStyle()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberStyleKey);
        }

        #endregion

        #region public string NumberPrefix {get;set;} + RemoveNumberPrefix()

        /// <summary>
        /// Gets or sets the number prefix for the page numbering
        /// </summary>
        [PDFAttribute("number-prefix")]
        [PDFDesignable("Number Prefix", Category = "Paper", Priority = 1)]
        public string NumberPrefix
        {
            get
            {
                string pref;
                if (this.TryGetValue(PDFStyleKeys.PageNumberPrefixKey, out pref))
                    return pref;
                else
                    return string.Empty;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberPrefixKey, value);
            }
        }

        /// <summary>
        /// Removes any explict page numbering on this style
        /// </summary>
        public void RemoveNumberPrefix()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberPrefixKey);
        }

        #endregion

        #region public int NumberStartIndex {get;set;}

        /// <summary>
        /// Gets or sets the stating page index number
        /// </summary>
        [PDFAttribute("number-start-index")]
        [PDFDesignable("Number Start Index", Category = "Paper", Priority = 1)]
        public int NumberStartIndex
        {
            get
            {
                int start;
                if (this.TryGetValue(PDFStyleKeys.PageNumberStartKey, out start))
                    return start;
                else
                    return 1;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberStartKey, value);
            }
        }

        /// <summary>
        /// Clears any existing value for the number start index from this style
        /// </summary>
        public void RemoveNumberStartIndex()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberStartKey);
        }

        #endregion

        #region public string NumberGroup {get;set;}

        /// <summary>
        /// Not currently used
        /// </summary>
        [PDFAttribute("number-group")]
        public string NumberGroup
        {
            get
            {
                string value;
                if (this.TryGetValue(PDFStyleKeys.PageNumberGroupKey, out value))
                    return value;
                else
                    return string.Empty;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberGroupKey, value);
            }
        }

        /// <summary>
        /// Clears any existing number group value from this style
        /// </summary>
        public void RemoveNumberGroup()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberGroupKey);
        }

        #endregion

        #region public string PageNumberFormat {get;set;} + RemovePageNumberFormat

        /// <summary>
        /// Gets or sets the page number format string for this page
        /// </summary>
        [PDFAttribute("display-format")]
        [PDFDesignable("Number Display Format", Category = "Paper", Priority = 1)]
        public string PageNumberFormat
        {
            get
            {
                string value;
                if (this.TryGetValue(PDFStyleKeys.PageNumberFormatKey, out value))
                    return value;
                else
                    return string.Empty;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberFormatKey, value);
            }
        }

        /// <summary>
        /// Clears any existing page number format value from this style
        /// </summary>
        public void RemovePageNumberFormat()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberFormatKey);
        }

        #endregion

        #region public int PageGroupCountHint {get;set;} + RemovePageGroupCountHint

        /// <summary>
        /// Gets or sets the hinted / approximate number of pages expected in this current page group
        /// </summary>
        [PDFAttribute("group-count-hint")]
        public int PageGroupCountHint
        {
            get
            {
                int value;
                if (this.TryGetValue(PDFStyleKeys.PageNumberGroupHintKey, out value))
                    return value;
                else
                    return -1;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberGroupHintKey, value);
            }
        }

        /// <summary>
        /// Clears any existing page number format value from this style
        /// </summary>
        public void RemovePageGroupCountHint()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberGroupHintKey);
        }

        #endregion

        #region public int PageTotalCountHint {get;set;} + RemovePageGroupCountHint

        /// <summary>
        /// Gets or sets the hinted / approximate number of pages expected in this document
        /// </summary>
        [PDFAttribute("total-count-hint")]
        public int PageTotalCountHint
        {
            get
            {
                int value;
                if (this.TryGetValue(PDFStyleKeys.PageNumberTotalHintKey, out value))
                    return value;
                else
                    return -1;
            }
            set
            {
                this.SetValue(PDFStyleKeys.PageNumberTotalHintKey, value);
            }
        }

        /// <summary>
        /// Clears any existing page count hint value from this style
        /// </summary>
        public void RemovePageTotalCountHint()
        {
            this.RemoveValue(PDFStyleKeys.PageNumberTotalHintKey);
        }

        #endregion

        #region public PageRotationAngles PageAngle {get;set;} + RemovePageAngle

        private static readonly int[] AllowedPageIndexValues = new int[] { 0, 90, 180, 270 };
        /// <summary>
        /// The angle of the page in increments of 90 degrees
        /// </summary>
        [PDFAttribute("page-angle")]
        [PDFDesignable("Page Rotation", Ignore = true,  Category = "Paper", Priority = 1, Type = "Select")]
        public PageRotationAngles PageAngle
        {
            get
            {
                int value;
                if (this.TryGetValue(PDFStyleKeys.PageAngle, out value))
                    return (PageRotationAngles)value;
                else
                    return 0;
            }
            set
            {
                if (Array.IndexOf(AllowedPageIndexValues, value) >= 0)
                    this.SetValue(PDFStyleKeys.PageAngle, (int)value);
            }
        }

        public void RemovePageAngle()
        {
            this.RemoveValue(PDFStyleKeys.PageAngle);
        }

        #endregion

        //
        // ctor
        //

        public PDFPageStyle()
            : base(PDFStyleKeys.PageItemKey)
        {
        }

        public PDFPageSize CreatePageSize()
        {
            return this.AssertOwner().DoCreatePageSize();
        }
    }
}
