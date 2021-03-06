﻿using System.Collections.Generic;

namespace org.apache.lucene.analysis.cjk
{

	/*
	 * Licensed to the Apache Software Foundation (ASF) under one or more
	 * contributor license agreements.  See the NOTICE file distributed with
	 * this work for additional information regarding copyright ownership.
	 * The ASF licenses this file to You under the Apache License, Version 2.0
	 * (the "License"); you may not use this file except in compliance with
	 * the License.  You may obtain a copy of the License at
	 *
	 *     http://www.apache.org/licenses/LICENSE-2.0
	 *
	 * Unless required by applicable law or agreed to in writing, software
	 * distributed under the License is distributed on an "AS IS" BASIS,
	 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	 * See the License for the specific language governing permissions and
	 * limitations under the License.
	 */

	using TokenFilterFactory = org.apache.lucene.analysis.util.TokenFilterFactory;

	/// <summary>
	/// Factory for <seealso cref="CJKBigramFilter"/>.
	/// <pre class="prettyprint">
	/// &lt;fieldType name="text_cjk" class="solr.TextField"&gt;
	///   &lt;analyzer&gt;
	///     &lt;tokenizer class="solr.StandardTokenizerFactory"/&gt;
	///     &lt;filter class="solr.CJKWidthFilterFactory"/&gt;
	///     &lt;filter class="solr.LowerCaseFilterFactory"/&gt;
	///     &lt;filter class="solr.CJKBigramFilterFactory" 
	///       han="true" hiragana="true" 
	///       katakana="true" hangul="true" outputUnigrams="false" /&gt;
	///   &lt;/analyzer&gt;
	/// &lt;/fieldType&gt;</pre>
	/// </summary>
	public class CJKBigramFilterFactory : TokenFilterFactory
	{
	  internal readonly int flags;
	  internal readonly bool outputUnigrams;

	  /// <summary>
	  /// Creates a new CJKBigramFilterFactory </summary>
	  public CJKBigramFilterFactory(IDictionary<string, string> args) : base(args)
	  {
		int flags = 0;
		if (getBoolean(args, "han", true))
		{
		  flags |= CJKBigramFilter.HAN;
		}
		if (getBoolean(args, "hiragana", true))
		{
		  flags |= CJKBigramFilter.HIRAGANA;
		}
		if (getBoolean(args, "katakana", true))
		{
		  flags |= CJKBigramFilter.KATAKANA;
		}
		if (getBoolean(args, "hangul", true))
		{
		  flags |= CJKBigramFilter.HANGUL;
		}
		this.flags = flags;
		this.outputUnigrams = getBoolean(args, "outputUnigrams", false);
		if (args.Count > 0)
		{
		  throw new System.ArgumentException("Unknown parameters: " + args);
		}
	  }

	  public override TokenStream create(TokenStream input)
	  {
		return new CJKBigramFilter(input, flags, outputUnigrams);
	  }
	}

}