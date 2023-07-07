using MediatR;
using Persistence;

public class Delete 
{
    public class Command: IRequest
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _dataContext;
        public Handler(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._dataContext.Activities.FindAsync(request.Id);
            this._dataContext.Remove(activity);
            await this._dataContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}