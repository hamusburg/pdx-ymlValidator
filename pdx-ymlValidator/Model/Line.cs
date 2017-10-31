namespace pdx_ymlValidator.Model
{
    /// <summary>
    /// YML行单位
    /// </summary>
    class Line
    {
        /// <summary>
        /// 构造Line
        /// </summary>
        /// <param name="key">索引值</param>
        /// <param name="value">内容值</param>
        public Line(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 与某一行进行对比
        /// </summary>
        /// <param name="reference">被对比的行</param>
        /// <returns>对比结果文本</returns>
        public string CompareWith(Line reference)
        {
            //TODO:实现逻辑
            return null;
        }

        /// <summary>
        /// 索引值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 内容值
        /// </summary>
        public string Value { get; set; }
    }
}
