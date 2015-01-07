﻿/*------------------------------------------------------------------------------
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
------------------------------------------------------------------------------*/

namespace StyleCopCmd.Writer.NUnit.Model
{

    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class failureType
    {
        private string messageField;
        private string stacktraceField;
        /// 
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("stack-trace")]
        public string stacktrace
        {
            get
            {
                return this.stacktraceField;
            }
            set
            {
                this.stacktraceField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class reasonType
    {
        private string messageField;
        /// 
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class resultsType
    {
        private object[] itemsField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("test-case", typeof(testcaseType))]
        [System.Xml.Serialization.XmlElementAttribute("test-suite", typeof(testsuiteType))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "test-caseType")]
    [System.Xml.Serialization.XmlRootAttribute("test-caseType", Namespace = "", IsNullable = true)]
    public partial class testcaseType
    {
        private categoryType[] categoriesField;
        private propertyType[] propertiesField;
        private object itemField;
        private string nameField;
        private string descriptionField;
        private string successField;
        private string timeField;
        private string executedField;
        private string assertsField;
        private string resultField;
        /// 
        [System.Xml.Serialization.XmlArrayItemAttribute("category", IsNullable = false)]
        public categoryType[] categories
        {
            get
            {
                return this.categoriesField;
            }
            set
            {
                this.categoriesField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlArrayItemAttribute("property", IsNullable = false)]
        public propertyType[] properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("failure", typeof(failureType))]
        [System.Xml.Serialization.XmlElementAttribute("reason", typeof(reasonType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string success
        {
            get
            {
                return this.successField;
            }
            set
            {
                this.successField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executed
        {
            get
            {
                return this.executedField;
            }
            set
            {
                this.executedField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string asserts
        {
            get
            {
                return this.assertsField;
            }
            set
            {
                this.assertsField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class categoryType
    {
        private string nameField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class propertyType
    {
        private string nameField;
        private string valueField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "test-suiteType")]
    [System.Xml.Serialization.XmlRootAttribute("test-suiteType", Namespace = "", IsNullable = true)]
    public partial class testsuiteType
    {
        private categoryType[] categoriesField;
        private propertyType[] propertiesField;
        private object itemField;
        private resultsType resultsField;
        private string typeField;
        private string nameField;
        private string descriptionField;
        private string successField;
        private string timeField;
        private string executedField;
        private string assertsField;
        private string resultField;
        /// 
        [System.Xml.Serialization.XmlArrayItemAttribute("category", IsNullable = false)]
        public categoryType[] categories
        {
            get
            {
                return this.categoriesField;
            }
            set
            {
                this.categoriesField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlArrayItemAttribute("property", IsNullable = false)]
        public propertyType[] properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("failure", typeof(failureType))]
        [System.Xml.Serialization.XmlElementAttribute("reason", typeof(reasonType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        /// 
        public resultsType results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string success
        {
            get
            {
                return this.successField;
            }
            set
            {
                this.successField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string executed
        {
            get
            {
                return this.executedField;
            }
            set
            {
                this.executedField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string asserts
        {
            get
            {
                return this.assertsField;
            }
            set
            {
                this.assertsField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class categoriesType
    {
        private categoryType[] categoryField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("category")]
        public categoryType[] category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class propertiesType
    {
        private propertyType[] propertyField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("property")]
        public propertyType[] property
        {
            get
            {
                return this.propertyField;
            }
            set
            {
                this.propertyField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class environmentType
    {
        private string nunitversionField;
        private string clrversionField;
        private string osversionField;
        private string platformField;
        private string cwdField;
        private string machinenameField;
        private string userField;
        private string userdomainField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("nunit-version")]
        public string nunitversion
        {
            get
            {
                return this.nunitversionField;
            }
            set
            {
                this.nunitversionField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("clr-version")]
        public string clrversion
        {
            get
            {
                return this.clrversionField;
            }
            set
            {
                this.clrversionField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("os-version")]
        public string osversion
        {
            get
            {
                return this.osversionField;
            }
            set
            {
                this.osversionField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string platform
        {
            get
            {
                return this.platformField;
            }
            set
            {
                this.platformField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cwd
        {
            get
            {
                return this.cwdField;
            }
            set
            {
                this.cwdField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("machine-name")]
        public string machinename
        {
            get
            {
                return this.machinenameField;
            }
            set
            {
                this.machinenameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string user
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("user-domain")]
        public string userdomain
        {
            get
            {
                return this.userdomainField;
            }
            set
            {
                this.userdomainField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "culture-infoType")]
    [System.Xml.Serialization.XmlRootAttribute("culture-infoType", Namespace = "", IsNullable = true)]
    public partial class cultureinfoType
    {
        private string currentcultureField;
        private string currentuicultureField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("current-culture")]
        public string currentculture
        {
            get
            {
                return this.currentcultureField;
            }
            set
            {
                this.currentcultureField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("current-uiculture")]
        public string currentuiculture
        {
            get
            {
                return this.currentuicultureField;
            }
            set
            {
                this.currentuicultureField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class resultType
    {
        private environmentType environmentField;
        private cultureinfoType cultureinfoField;
        private testsuiteType testsuiteField;
        private string nameField;
        private decimal totalField;
        private decimal errorsField;
        private decimal failuresField;
        private decimal inconclusiveField;
        private decimal notrunField;
        private decimal ignoredField;
        private decimal skippedField;
        private decimal invalidField;
        private string dateField;
        private string timeField;
        /// 
        public environmentType environment
        {
            get
            {
                return this.environmentField;
            }
            set
            {
                this.environmentField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("culture-info")]
        public cultureinfoType cultureinfo
        {
            get
            {
                return this.cultureinfoField;
            }
            set
            {
                this.cultureinfoField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("test-suite")]
        public testsuiteType testsuite
        {
            get
            {
                return this.testsuiteField;
            }
            set
            {
                this.testsuiteField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal errors
        {
            get
            {
                return this.errorsField;
            }
            set
            {
                this.errorsField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal failures
        {
            get
            {
                return this.failuresField;
            }
            set
            {
                this.failuresField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal inconclusive
        {
            get
            {
                return this.inconclusiveField;
            }
            set
            {
                this.inconclusiveField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute("not-run")]
        public decimal notrun
        {
            get
            {
                return this.notrunField;
            }
            set
            {
                this.notrunField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ignored
        {
            get
            {
                return this.ignoredField;
            }
            set
            {
                this.ignoredField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal skipped
        {
            get
            {
                return this.skippedField;
            }
            set
            {
                this.skippedField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal invalid
        {
            get
            {
                return this.invalidField;
            }
            set
            {
                this.invalidField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }
    }
}