import { D_OptionalAggregate } from './d-optional-aggregate.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('d_multiple_dependent')
export class D_MultipleDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  multipleDepAttr: string;

  @OneToMany(() => D_OptionalAggregate, (d_OptionalAggregates) => d_OptionalAggregates.d_MultipleDependent, { cascade: ['insert', 'update'] })
  d_OptionalAggregates: D_OptionalAggregate[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}