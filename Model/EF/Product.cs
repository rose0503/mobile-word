namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    
     public partial class Product
    {
        public long ID { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Độ dài mật khẩu ít nhất 5 và nhỏ hơn 250 ký tự.")]
        public string Name { get; set; }

        [Display(Name = "Tên SEO")]
        [Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm SEO")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Độ dài mật khẩu ít nhất 5 và nhỏ hơn 250 ký tự.")]        
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Yêu cầu viết mô tả sản phẩm")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Độ dài mật khẩu ít nhất 10 ký tự.")]        
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Yêu cầu chọn hình sản phẩm.")]
        [StringLength(140)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Yêu cầu nhập giá sản phẩm.")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { get; set; }

        public bool? IncludeVAT { get; set; }

        [Display(Name = "Số lượng")]        
        public int? Quantily { get; set; }

        [Display(Name = "Hãng ")]        
        public long? CategoryID { get; set; }

        [Display(Name = "Bảo hành")]
        [Required(ErrorMessage = "Yêu cầu nhập năm bảo hành.")]
        public int? Warranty { get; set; }

        [Display(Name = "Kích thước")]
        [Required(ErrorMessage = "Yêu cầu nhập kích thước.")]        
        public string Size { get; set; }

        [Display(Name = "Bộ nhớ trong")]
        [Required(ErrorMessage = "Yêu cầu nhập bộ nhớ trong.")]
        public int? Memory { get; set; }

        [Display(Name = "Trọng lượng")]
        [Required(ErrorMessage = "Yêu cầu nhập trọng lượng.")]
        public decimal? Weight { get; set; }

        [Display(Name = "Màu")]
        [Required(ErrorMessage = "Yêu cầu nhập màu.")]
        [StringLength(50)]
        public string Color { get; set; }

        [Display(Name = "Chip")]
        [Required(ErrorMessage = "Yêu cầu nhập Chip.")]
        public string Chip { get; set; }

        [Display(Name = "Tốc độ CPU")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin CPU.")]
        public string CPU { get; set; }

        [Display(Name = "Hệ điều hành")]
        [Required(ErrorMessage = "Yêu cầu nhập hệ điều hành.")]
        public string OS { get; set; }

        [Display(Name = "RAM")]
        [Required(ErrorMessage = "Yêu cầu nhập RAM.")]        
        public decimal? Ram { get; set; }


        [Display(Name = "Thông tin pin")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin pin.")]
        public string Battery { get; set; }

        [Display(Name = "Thông tin màn hình")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin màn hình.")]
        public string Screen { get; set; }

        [Display(Name = "Camera trước")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin camera trước.")]
        public string FontCamera { get; set; }

        [Display(Name = "Camera sau 1")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin camera sau 1.")]
        public string RearCameraNo1 { get; set; }

        [Display(Name = "Camera sau 2")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin camera sau 2.")]
        public string RearCameraNo2 { get; set; }

        [Display(Name = "Sim")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin sim.")]
        [Column(TypeName = "text")]
        public string Sim { get; set; }

        [Display(Name = "Kết nối")]
        [Required(ErrorMessage = "Yêu cầu nhập thông tin kết nối.")]        
        public string Connect { get; set; }

        [Display(Name = "Tính năng đặc biệt")]
        [Required(ErrorMessage = "Yêu cầu nhập tính năng đặc biệt")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Độ dài mật khẩu ít nhất 5 ký tự.")]
        public string SpecialFeatures { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }


        public bool Status { get; set; }

        [Display(Name = "Ngày lên hot")]
        [Required(ErrorMessage = "Yêu cầu nhập ngày")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        public bool? IsDelete { get; set; }
        
    }
}
