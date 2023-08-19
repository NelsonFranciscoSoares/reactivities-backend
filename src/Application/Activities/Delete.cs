using System.Diagnostics;
using Application.Core;
using MediatR;
using Persistence;

public class Delete 
{
    public class Command: IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        public Handler(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await this._dataContext.Activities.FindAsync(request.Id);

            // if (activity == null) return null;

            this._dataContext.Remove(activity);

            var result = await this._dataContext.SaveChangesAsync() > 0;

            if (!result) Result<Unit>.Failure("Failed to remove activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}