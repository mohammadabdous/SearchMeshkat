﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.AR;
using Lucene.Net.Analysis;
using System.IO;

namespace Indexer
{
    public class ArabicAnalyzerPlus : Analyzer
    {
        private readonly Lucene.Net.Util.Version _version;
        private ISet<string> stopWords;
        public ArabicAnalyzerPlus(Lucene.Net.Util.Version version, ISet<string> sw)
        {
            _version = version;
            stopWords = sw;
        }
        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            TokenStream result = new ArabicLetterTokenizer(reader);
            result = new StopFilter(true, result, stopWords);
            result = new ArabicPlusNormalizationFilter(result);
            result = new ArabicNormalizationFilter(result);
            result = new ArabicStemFilter(result);
            return result; 
        }
    }
}
