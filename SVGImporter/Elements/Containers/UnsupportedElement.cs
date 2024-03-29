﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGImporter.Elements;

namespace SVGImporter.Elements.Containers
{
    internal class UnsupportedElement : Group
    {
        private string tagName;

        internal UnsupportedElement(List<TagAttribute> attributes, string localName) : base(attributes)
        {
            this.tagName = localName;
        }
        protected override string GetStartTag()
        {
            return $"<{tagName} {AttributesToSVG(new List<string>())}>";
        }
        protected override string GetEndTag()
        {
            return $"</{tagName}>";
        }
        public override TagType GetTagType()
        {
            return TagType.Unknown;
        }
    }
}
