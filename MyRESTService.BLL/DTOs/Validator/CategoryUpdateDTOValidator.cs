using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyRESTService.BLL.DTOs.Validator
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Isi Bro");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Isi Bro");
        }
    }
}
