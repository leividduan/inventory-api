﻿using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace Inventory.Domain.Entities;

public abstract class Entity
{
	protected Entity()
	{
		var now = DateTime.Now;
		CreatedAt = now;
		UpdatedAt = now;

		ValidationResult = new ValidationResult();
	}

	public int Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	[NotMapped] public ValidationResult ValidationResult { get; set; }

	public abstract bool IsValid();

	public Error? GetErrors()
	{
		var errorsDetail = ValidationResult.Errors.GroupBy(x => new { x.PropertyName })
			.Select(x => new ErrorDetails(x.Key.PropertyName, x.Select(s => s.ErrorMessage).ToList())).ToList();

		if (errorsDetail.Count == 0)
			return null;

		var errors = new Error(errorsDetail);

		return errors;
	}
}
