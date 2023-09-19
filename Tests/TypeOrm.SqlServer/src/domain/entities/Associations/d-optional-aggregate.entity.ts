import { D_MultipleDependent } from './d-multiple-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('d_optional_aggregate')
export class D_OptionalAggregate {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  optionalAggrAttr: string;

  @ManyToOne(() => D_MultipleDependent, (d_MultipleDependent) => d_MultipleDependent.d_OptionalAggregates, { cascade: ['insert', 'update'] })
  d_MultipleDependent?: D_MultipleDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}