using System.ComponentModel.DataAnnotations;

namespace BlazorManager.Data.Models
{
    public class ExampleModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        [Required(ErrorMessage = "제목을 입력해주세요")]
        public string Title { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        [Required(ErrorMessage = "내용을 입력해주세요")]
        public string Content { get; set; }

    }
}
