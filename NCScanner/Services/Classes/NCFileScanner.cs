using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using NCScanner.DataTypes;
using NCScanner.Resources;
using NCScanner.Services.Interfaces;

namespace NCScanner.Services.Classes
{
    class NCFileScanner : INCFileScanner
    {
        private List<string> tools;

        private List<double> xPositions;

        private List<double> yPositions;

        private List<double> zPositions;

        public NCFileScanner()
        {
            tools = new List<string>();

            xPositions = new List<double>();
            yPositions = new List<double>();
            zPositions = new List<double>();
        }

        public NCData ScanNCFile(string filePath)
        {
            var toolRegex = new Regex(Strings.ToolRegex, RegexOptions.IgnoreCase);
            var positionRegex = new Regex(Strings.PositionRegex, RegexOptions.IgnoreCase);

            foreach (string line in File.ReadLines(filePath))
            {
                var toolMatch = toolRegex.Match(line);
                var positionMatch = positionRegex.Match(line);

                if (toolMatch.Success)
                {
                    tools.Add(toolMatch.Value);
                }
                else if (positionMatch.Success)
                {
                    AddPositionsToList(positionMatch.Groups);
                }
            }

            return SetNCData();
        }

        private void AddPositionsToList(GroupCollection positionsGroup)
        {
            if (!string.IsNullOrWhiteSpace(positionsGroup[Strings.XPositionGroup].Value))
            {
                xPositions.Add(Convert.ToDouble(positionsGroup[Strings.XPositionGroup].Value));
            }
            if (!string.IsNullOrWhiteSpace(positionsGroup[Strings.YPositionGroup].Value))
            {
                yPositions.Add(Convert.ToDouble(positionsGroup[Strings.YPositionGroup].Value));
            }
            if (!string.IsNullOrWhiteSpace(positionsGroup[Strings.ZPositionGroup].Value))
            {
                zPositions.Add(Convert.ToDouble(positionsGroup[Strings.ZPositionGroup].Value));
            }
        }

        private string GenerateToolList()
        {
            var toolList = string.Empty;
            foreach (var tool in tools.Distinct())
            {
                toolList += $"{tool}{Environment.NewLine}";
            }

            return toolList;
        }

        private NCData SetNCData()
        {
            return new NCData()
            {
                ToolList = GenerateToolList(),
                XMin = xPositions.Min(),
                YMin = yPositions.Min(),
                ZMin = zPositions.Min(),
                XMax = xPositions.Max(),
                YMax = yPositions.Max(),
                ZMax = zPositions.Max()
            };
        }
    }
}
