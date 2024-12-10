# Quản Lý Dự Án Cho Sinh Viên UTC2
 Với mô hình microservice: được sử dụng C# + Python + React
 Với công nghệ C# sử dụng dotnet 8.0 cùng CQRS Pattern giúp tách phần command và query làm 2 phần tách biệt nhau
 Với thư viện react version 18 + vite + bootstrap giúp frontend đẹp mắt hơn thuận tiện hơn
 Dữ liệu được tốn ưu ở SQL SERVER thông qua các cách sử dụng index giúp việc tốc độ truy vấn nhanh chóng hơn
 Với Redis giúp lưu những thông tin truy vấn liên tục giúp hạn chế việc truy cập database giúp tốc độ truy vấn nhanh chóng
 Sử dụng các thuật toán học máy: deduplication -> giúp hạn chế những thông tin trùng lặp
  + Xây dựng Content-based Filtering RS [Recommender System] -> Nó sẽ gợi ý các item dựa trên hồ sơ (profiles) của người dùng hoặc dựa vào nội dung, thuộc tính (attributes) của những item tương tự như item mà người dùng đã chọn trong quá khứ
 
