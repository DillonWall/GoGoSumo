﻿// Refs: 
// https://stackoverflow.com/questions/8902674/manually-map-column-names-with-class-properties
// https://gist.github.com/kalebpederson/5460509

using Dapper;
using System.Reflection;

namespace GoGoSumo.DTOs.Helpers.DataAnnotations;

/// <summary>
/// Uses the Name value of the <see cref="ColumnAttribute"/> specified to determine
/// the association between the name of the column in the query results and the member to
/// which it will be extracted. If no column mapping is present all members are mapped as
/// usual.
/// </summary>
/// <typeparam name="T">The type of the object that this association between the mapper applies to.</typeparam>
public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
{
    public ColumnAttributeTypeMapper()
        : base(new SqlMapper.ITypeMap[]
            {
                new CustomPropertyTypeMap(
                    typeof(T),
                    (type, columnName) =>
                        type.GetProperties()
                            .FirstOrDefault(prop =>
                                prop.GetCustomAttributes(false)?
                                    .OfType<ColumnAttribute>()
                                    .Any(attr => attr.Name == columnName)
                                ?? false
                            )!
                ),
                new DefaultTypeMap(typeof(T))
            }
        )
    { }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class ColumnAttribute : Attribute
{
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public ColumnAttribute(string name) : base()
    {
        Name = name;
    }
    public required string Name { get; set; }
}

public class FallbackTypeMapper : SqlMapper.ITypeMap
{
    private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

    public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
    {
        _mappers = mappers;
    }


    public ConstructorInfo? FindConstructor(string[] names, Type[] types)
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                ConstructorInfo? result = mapper.FindConstructor(names, types);
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }
        return null;
    }

    public SqlMapper.IMemberMap? GetConstructorParameter(ConstructorInfo constructor, string columnName)
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                var result = mapper.GetConstructorParameter(constructor, columnName);
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }
        return null;
    }

    public SqlMapper.IMemberMap? GetMember(string columnName)
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                var result = mapper.GetMember(columnName);
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }
        return null;
    }


    public ConstructorInfo? FindExplicitConstructor()
    {
        return _mappers
            .Select(mapper => mapper.FindExplicitConstructor())
            .FirstOrDefault(result => result != null);
    }
}