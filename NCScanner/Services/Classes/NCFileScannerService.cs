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
    class NCFileScannerService : INCFileScannerService
    {
        private List<string> tools;

        private List<string> workOffsets;

        private List<double> xPositions;

        private List<double> yPositions;

        private List<double> zPositions;

        public NCFileScannerService()
        {
            tools = new List<string>();
            workOffsets = new List<string>();

            xPositions = new List<double>();
            yPositions = new List<double>();
            zPositions = new List<double>();
        }

        public NCData ScanNCFile(string filePath)
        {
            var toolRegex = new Regex(Strings.ToolRegex, RegexOptions.IgnoreCase);
            var workOffsetRegex = new Regex(Strings.WorkOffsetRegex, RegexOptions.IgnoreCase);
            var positionRegex = new Regex(Strings.PositionRegex, RegexOptions.IgnoreCase);

            foreach (string line in File.ReadLines(filePath))
            {
                var toolMatch = toolRegex.Match(line);
                var workOffsetMatch = workOffsetRegex.Match(line);
                var positionMatch = positionRegex.Match(line);

                if (toolMatch.Success)
                {
                    tools.Add($"{toolMatch.Groups[2]} {toolMatch.Groups[5]}");
                }
                if (workOffsetMatch.Success)
                {
                    workOffsets.Add(workOffsetMatch.Value);
                }
                if (positionMatch.Success)
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

        private string GenerateToolString()
        {
            var toolString = string.Empty;
            foreach (var tool in tools.Distinct())
            {
                toolString += $"{tool}{Environment.NewLine}";
            }

            return toolString;
        }

        private string GenerateWorkOffsetString()
        {
            var workOffsetString = string.Empty;
            foreach (var workOffset in workOffsets.Distinct())
            {
                workOffsetString += $"{workOffset}{Environment.NewLine}";
            }

            return workOffsetString;
        }

        private NCData SetNCData()
        {
            return new NCData()
            {
                Tools = tools.Distinct().ToList(),
                ToolString = GenerateToolString(),
                WorkOffsets = workOffsets.Distinct().ToList(),
                WorkOffsetString = GenerateWorkOffsetString(),
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
