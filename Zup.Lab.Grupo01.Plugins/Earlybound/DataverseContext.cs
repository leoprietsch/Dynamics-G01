#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

namespace DataverseModel
{
	
	
	/// <summary>
	/// Represents a source of entities bound to a Dataverse service. It tracks and manages changes made to the retrieved entities.
	/// </summary>
	public partial class DataverseContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		public DataverseContext(Microsoft.Xrm.Sdk.IOrganizationService service) : 
				base(service)
		{
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.Account"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.Account> AccountSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.Account>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.Contact"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.Contact> ContactSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.Contact>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.Queue"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.Queue> QueueSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.Queue>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.zup_categoria"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.zup_categoria> zup_categoriaSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.zup_categoria>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.zup_classificacao"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.zup_classificacao> zup_classificacaoSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.zup_classificacao>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.zup_lab_ocorrencia"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.zup_lab_ocorrencia> zup_lab_ocorrenciaSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.zup_lab_ocorrencia>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.zup_subcategoria"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.zup_subcategoria> zup_subcategoriaSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.zup_subcategoria>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.zup_subcategoria_Queue"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.zup_subcategoria_Queue> zup_subcategoria_QueueSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.zup_subcategoria_Queue>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="DataverseModel.zup_virtual_post"/> entities.
		/// </summary>
		public System.Linq.IQueryable<DataverseModel.zup_virtual_post> zup_virtual_postSet
		{
			get
			{
				return this.CreateQuery<DataverseModel.zup_virtual_post>();
			}
		}
	}
	
	/// <summary>
	/// Attribute to handle storing the OptionSet's Metadata.
	/// </summary>
	[System.AttributeUsageAttribute(System.AttributeTargets.Field)]
	public sealed class OptionSetMetadataAttribute : System.Attribute
	{
		
		private object[] _nameObjects;
		
		private System.Collections.Generic.Dictionary<int, string> _names;
		
		/// <summary>
		/// Color of the OptionSetValue.
		/// </summary>
		public string Color { get; set; }
		
		/// <summary>
		/// Description of the OptionSetValue.
		/// </summary>
		public string Description { get; set; }
		
		/// <summary>
		/// Display order index of the OptionSetValue.
		/// </summary>
		public int DisplayIndex { get; set; }
		
		/// <summary>
		/// External value of the OptionSetValue.
		/// </summary>
		public string ExternalValue { get; set; }
		
		/// <summary>
		/// Name of the OptionSetValue.
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Names of the OptionSetValue.
		/// </summary>
		public System.Collections.Generic.Dictionary<int, string> Names
		{
			get
			{
				return _names ?? (_names = CreateNames());
			} 
			set
			{
				_names = value;
				if (value == null)
				{
				    _nameObjects = new object[0];
				}
				else
				{
				    _nameObjects = null;
				}
			}
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="OptionSetMetadataAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the value.</param>
		/// <param name="displayIndex">Display order index of the value.</param>
		/// <param name="color">Color of the value.</param>
		/// <param name="description">Description of the value.</param>
		/// <param name="externalValue">External value of the value.</param>
		/// <param name="names">Names of the value.</param>
		public OptionSetMetadataAttribute(string name, int displayIndex, string color = null, string description = null, string externalValue = null, params object[] names)
		{
			this.Color = color;
			this.Description = description;
			this._nameObjects = names;
			this.ExternalValue = externalValue;
			this.DisplayIndex = displayIndex;
			this.Name = name;
		}
		
		private System.Collections.Generic.Dictionary<int, string> CreateNames()
		{
			System.Collections.Generic.Dictionary<int, string> names = new System.Collections.Generic.Dictionary<int, string>();
			for (int i = 0; (i < _nameObjects.Length); i = (i + 2))
			{
				names.Add(((int)(_nameObjects[i])), ((string)(_nameObjects[(i + 1)])));
			}
			return names;
		}
	}
	
	/// <summary>
	/// Extension class to handle retrieving of OptionSetMetadataAttribute.
	/// </summary>
	public static class OptionSetExtension
	{
		
		/// <summary>
		/// Returns the OptionSetMetadataAttribute for the given enum value
		/// </summary>
		/// <typeparam name="T">OptionSet Enum Type</typeparam>
		/// <param name="value">Enum Value with OptionSetMetadataAttribute</param>
		public static OptionSetMetadataAttribute GetMetadata<T>(this T value)
			where T :  struct, System.IConvertible
		{
			System.Type enumType = typeof(T);
			if (!enumType.IsEnum)
			{
				throw new System.ArgumentException("T must be an enum!");
			}
			System.Reflection.MemberInfo[] members = enumType.GetMember(value.ToString());
			for (int i = 0; (i < members.Length); i++
			)
			{
				System.Attribute attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(members[i], typeof(OptionSetMetadataAttribute));
				if (attribute != null)
				{
					return ((OptionSetMetadataAttribute)(attribute));
				}
			}
			throw new System.ArgumentException("T must be an enum adorned with an OptionSetMetadataAttribute!");
		}
	}
}
#pragma warning restore CS1591
