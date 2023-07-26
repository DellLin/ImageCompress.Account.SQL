using Grpc.Core;
using ImageCompress.AccountSQL.DBContents;
using ImageCompress.AccountSQL.DBModels.ImageCompress;
using Newtonsoft.Json;

namespace ImageCompress.AccountSQL.Services;

public class AccountService : AccountSQL.AccountService.AccountServiceBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly PostgresContext _postgresContext;

    public AccountService(ILogger<GreeterService> logger,
        PostgresContext postgresContext)
    {
        _logger = logger;
        _postgresContext = postgresContext;
    }
    public override async Task<InsertAccountReply> InsertAccount(InsertAccountRequest request, ServerCallContext context)
    {

        var account = new Account();
        account.Id = new Guid(request.Account.Id);
        account.Email = request.Account.Email;
        account.Password = request.Account.Password;
        account.GoogleId = request.Account.GoogleId;
        account.LineId = request.Account.LineId;
        account.State = request.Account.State;
        account.CreateDate = ConvertTool.ConvertToDatetime(request.Account.CreateDate);
        account.CreateBy = ConvertTool.ConvertToGuid(request.Account.CreateBy);
        account.UpdateDate = ConvertTool.ConvertToDatetime(request.Account.UpdateDate);
        account.UpdateBy = ConvertTool.ConvertToGuid(request.Account.UpdateBy);
        await _postgresContext.Account.AddAsync(account);
        await _postgresContext.SaveChangesAsync();
        return new InsertAccountReply()
        {
            State = true,
        };
    }

    public override async Task<SelectAccountByIdReply> SelectAccountById(SelectAccountByIdRequest request, ServerCallContext context)
    {
        var accountItem = new AccountItem();
        var account = await _postgresContext.Account.FindAsync(new Guid(request.Id));
        if (account != null)
        {
            accountItem.Id = account.Id.ToString();
            accountItem.Email = account.Email ?? "";
            accountItem.Password = account.Password ?? "";
            accountItem.GoogleId = account.GoogleId ?? "";
            accountItem.LineId = account.LineId ?? "";
            accountItem.State = account.State ?? 0;
            accountItem.CreateDate = account.CreateDate.ToString() ?? "";
            accountItem.CreateBy = account.CreateBy.ToString() ?? "";
            accountItem.UpdateDate = account.UpdateDate.ToString() ?? "";
            accountItem.UpdateBy = account.UpdateBy.ToString() ?? "";
        }
        return new SelectAccountByIdReply()
        {
            Account = accountItem
        };
    }
    public override async Task<SelectAccountByEmailReply> SelectAccountByEmail(SelectAccountByEmailRequest request, ServerCallContext context)
    {
        var accountItem = new AccountItem();
        var account = _postgresContext.Account.FirstOrDefault(t => t.Email == request.Email);
        if (account != null)
        {
            accountItem.Id = account.Id.ToString();
            accountItem.Email = account.Email ?? "";
            accountItem.Password = account.Password ?? "";
            accountItem.GoogleId = account.GoogleId ?? "";
            accountItem.LineId = account.LineId ?? "";
            accountItem.State = account.State ?? 0;
            accountItem.CreateDate = account.CreateDate.ToString() ?? "";
            accountItem.CreateBy = account.CreateBy.ToString() ?? "";
            accountItem.UpdateDate = account.UpdateDate.ToString() ?? "";
            accountItem.UpdateBy = account.UpdateBy.ToString() ?? "";
        }
        return new SelectAccountByEmailReply()
        {
            Account = accountItem
        };
    }
    public override async Task<UpdateAccountReply> UpdateAccount(UpdateAccountRequest request, ServerCallContext context)
    {
        var account = await _postgresContext.Account.FindAsync(new Guid(request.Account.Id));
        if (account == null)
        {
            return new UpdateAccountReply()
            {
                State = false,
                Message = "Account not find."
            };
        }
        account.Id = new Guid(request.Account.Id);
        account.Email = request.Account.Email;
        account.GoogleId = request.Account.GoogleId;
        account.LineId = request.Account.LineId;
        account.State = request.Account.State;
        account.CreateDate = Convert.ToDateTime(request.Account.CreateDate);
        account.CreateBy = new Guid(request.Account.CreateBy);
        account.UpdateDate = Convert.ToDateTime(request.Account.UpdateDate);
        account.UpdateBy = new Guid(request.Account.UpdateBy);
        await _postgresContext.SaveChangesAsync();
        return new UpdateAccountReply()
        {
            State = true,
        };
    }
    public override async Task<DeleteAccountReply> DeleteAccount(DeleteAccountRequest request, ServerCallContext context)
    {
        var account = await _postgresContext.Account.FindAsync(new Guid(request.Account.Id));
        if (account == null)
        {
            return new DeleteAccountReply()
            {
                State = false,
                Message = "Account not find."
            };
        }
        _postgresContext.Account.Remove(account);
        await _postgresContext.SaveChangesAsync();
        return new DeleteAccountReply()
        {
            State = true,
        };
    }
}
