using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskDB;

public class UserMap:IEntityTypeConfiguration<User>{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x=>x.id);
        builder.HasMany(x=>x.Jobs);
    }
}