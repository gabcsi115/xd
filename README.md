Backend: 
- Scaffold-DbContext "SERVER=localhost;PORT=3306;DATABASE=halak;USER=root;PASSWORD=;SSL MODE=none;" mysql.entityframeworkcore -outputdir Models -f 

- builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
- var app = builder.Build();
- app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

Frontend: 
- npx create-react-app .
- npm uninstall web-vitals
- npm install axios react-router-dom bootstrap bootstrap-icons


